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
}
