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

    public async Task<ServiceResult<RoomDetailDTO>> GetRoomByIdAsync(int id)
    {
        try
        {
            var room = await _context.Rooms
                .Include(t => t.Hotel)
                .Include(t => t.Reservations)
                .FirstOrDefaultAsync(t => t.RoomId == id);

            if (room == null)
            {
                return new ServiceResult<RoomDetailDTO> { Success = false, ErrorMessage = "Hotel not found." };
            }

            return new ServiceResult<RoomDetailDTO> { Success = true, Data = _mapper.Map<RoomDetailDTO>(room) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<RoomDetailDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }
}
