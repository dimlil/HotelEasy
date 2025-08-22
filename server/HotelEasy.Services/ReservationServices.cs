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
            var rooms = await _context.Reservations
            .ToListAsync();
            return new ServiceResult<List<ReservationDTO>> { Success = true, Data = rooms.Select(t => _mapper.Map<ReservationDTO>(t)).ToList() };
        }
        catch (Exception ex)
        {
            return new ServiceResult<List<ReservationDTO>> { Success = false, ErrorMessage = ex.Message };
        }
    }
}
