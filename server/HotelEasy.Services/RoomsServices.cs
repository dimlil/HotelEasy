using AutoMapper;
using HotelEasy.Data;
using HotelEasy.Entities;
using HotelEasy.Services.Common;
using HotelEasy.Services.DTO;
using Microsoft.EntityFrameworkCore;

namespace HotelEasy.Services;

public class RoomsServices
{
    private readonly Diplomna21180105Context _context;
    private readonly IMapper _mapper;

    public RoomsServices(Diplomna21180105Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<RoomDTO>>> GetAllRoomsAsync()
    {
        try
        {
            var rooms = await _context.Rooms
            .Include(t => t.Hotel)
            .ToListAsync();
            return new ServiceResult<List<RoomDTO>> { Success = true, Data = rooms.Select(t => _mapper.Map<RoomDTO>(t)).ToList() };
        }
        catch (Exception ex)
        {
            return new ServiceResult<List<RoomDTO>> { Success = false, ErrorMessage = ex.Message };
        }
    }
}
