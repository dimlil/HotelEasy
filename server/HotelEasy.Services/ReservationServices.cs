using AutoMapper;
using HotelEasy.Data;
using HotelEasy.Entities;
using HotelEasy.Services.Common;
using HotelEasy.Services.DTO;
using Microsoft.EntityFrameworkCore;
using dotenv.net;

namespace HotelEasy.Services;

public class ReservationServices
{
    private readonly Diplomna21180105Context _context;
    private readonly IMapper _mapper;

    private readonly PaymentsServices _paymentService;

    public ReservationServices(Diplomna21180105Context context, IMapper mapper, PaymentsServices paymentService)
    {
        _context = context;
        _mapper = mapper;
        _paymentService = paymentService;
    }

    public async Task<ServiceResult<List<ReservationDTO>>> GetAllReservationAsync()
    {
        try
        {
            var reservation = await _context.Reservations
            .Include(t => t.Room)
                .ThenInclude(room => room.Hotel)
            .Include(t => t.User)
            .ToListAsync();

            return new ServiceResult<List<ReservationDTO>> { Success = true, Data = reservation.Select(t => _mapper.Map<ReservationDTO>(t)).ToList() };
        }
        catch (Exception ex)
        {
            return new ServiceResult<List<ReservationDTO>> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<ReservationDTO>> GetReservationByIdAsync(int id)
    {
        try
        {
            var reservation = await _context.Reservations
                .Include(t => t.Room)
                    .ThenInclude(room => room.Hotel)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.RoomId == id);

            if (reservation == null)
            {
                return new ServiceResult<ReservationDTO> { Success = false, ErrorMessage = "Reservation not found." };
            }

            return new ServiceResult<ReservationDTO> { Success = true, Data = _mapper.Map<ReservationDTO>(reservation) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<ReservationDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<string>> CreateReservationAsync(CreateReservationDTO dto)
    {
        try
        {
            var room = await _context.Rooms.FindAsync(dto.RoomId);
            if (room == null)
            {
                return ServiceResult<string>.Failure("Room not found.");
            }

            var reservation = _mapper.Map<Reservation>(dto);
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            DotEnv.Load();
            string clientUrl = Environment.GetEnvironmentVariable("CLIENT_URL") ?? "http://localhost:5173";
            string successUrl = $"{clientUrl}/success?reservationId={reservation.ReservationId}";
            string cancelUrl = $"{clientUrl}/cancel?reservationId={reservation.ReservationId}";

            var paymentResult = await _paymentService.CreateCheckoutSessionAsync(
                amount: room.Price,
                title: $"Reservation for room {room.RoomNumber}",
                successUrl: successUrl,
                cancelUrl: cancelUrl
            );

            if (!paymentResult.Success)
            {
                return ServiceResult<string>.Failure(paymentResult.ErrorMessage!);
            }

            return new ServiceResult<string> { Success = true, Data = paymentResult.Data! };
        }
        catch (Exception ex)
        {
            return ServiceResult<string>.Failure(ex.Message);
        }
    }


    public async Task<ServiceResult<ReservationDTO>> UpdateReservationAsync(int id, CreateReservationDTO dto)
    {
        try
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return new ServiceResult<ReservationDTO> { Success = false, ErrorMessage = "Reservation not found." };
            }

            _mapper.Map(dto, reservation);
            await _context.SaveChangesAsync();
            return new ServiceResult<ReservationDTO> { Success = true, Data = _mapper.Map<ReservationDTO>(reservation) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<ReservationDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<bool>> DeleteReservationAsync(int id)
    {
        try
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return new ServiceResult<bool> { Success = false, ErrorMessage = "Reservation not found." };
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return new ServiceResult<bool> { Success = true, Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResult<bool> { Success = false, ErrorMessage = ex.Message };
        }
    }
}
