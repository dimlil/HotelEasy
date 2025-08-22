using AutoMapper;
using HotelEasy.Data;
using HotelEasy.Entities;
using HotelEasy.Services.Common;
using HotelEasy.Services.DTO;
using Microsoft.EntityFrameworkCore;

namespace HotelEasy.Services;

public class ReservationServices
{
    private readonly Diplomna21180105Context _context;
    private readonly IMapper _mapper;

    public ReservationServices(Diplomna21180105Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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

    public async Task<ServiceResult<ReservationDTO>> CreateReservationAsync(CreateReservationDTO dto)
    {
        try
        {
            var reservation = _mapper.Map<Reservation>(dto);

            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return new ServiceResult<ReservationDTO> { Success = true, Data = _mapper.Map<ReservationDTO>(reservation) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<ReservationDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }
}
