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
                return new ServiceResult<RoomDetailDTO> { Success = false, ErrorMessage = "Room not found." };
            }

            var owner = await _context.Users.FindAsync(room.Hotel.OwnerId);
            if (owner == null)
            {
                return ServiceResult<RoomDetailDTO>.Failure("User not found");
            }

            room.Hotel.Owner = owner;

            return new ServiceResult<RoomDetailDTO> { Success = true, Data = _mapper.Map<RoomDetailDTO>(room) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<RoomDetailDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<RoomDTO>> CreateRoomAsync(CreateRoomDTO dto)
    {
        try
        {
            var room = _mapper.Map<Room>(dto);
            var hotel = await _context.Hotels.FindAsync(dto.HotelId);

            if (hotel == null)
                return ServiceResult<RoomDTO>.Failure("Hotel not found");

            room.HotelId = hotel.HotelId;
            room.Hotel = hotel;

            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return new ServiceResult<RoomDTO> { Success = true, Data = _mapper.Map<RoomDTO>(room) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<RoomDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<RoomDTO>> UpdateRoomAsync(int id, CreateRoomDTO dto)
    {
        try
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return new ServiceResult<RoomDTO> { Success = false, ErrorMessage = "Room not found." };
            }

            var hotel = await _context.Hotels.FindAsync(dto.HotelId);

            if (hotel == null)
                return ServiceResult<RoomDTO>.Failure("Hotel not found");

            room.HotelId = hotel.HotelId;

            var owner = await _context.Users.FindAsync(hotel.OwnerId);
            if (owner == null)
            {
                return ServiceResult<RoomDTO>.Failure("User not found");
            }

            hotel.Owner = owner;
            room.Hotel = hotel;

            _mapper.Map(dto, room);
            await _context.SaveChangesAsync();
            return new ServiceResult<RoomDTO> { Success = true, Data = _mapper.Map<RoomDTO>(room) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<RoomDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<bool>> DeleteRoomAsync(int id)
    {
        try
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return new ServiceResult<bool> { Success = false, ErrorMessage = "Room not found." };
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return new ServiceResult<bool> { Success = true, Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResult<bool> { Success = false, ErrorMessage = ex.Message };
        }
    }
}
