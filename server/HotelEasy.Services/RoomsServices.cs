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

    private readonly CloudinaryImageService _imageService;

    public RoomsServices(Diplomna21180105Context context, IMapper mapper, CloudinaryImageService imageService)
    {
        _context = context;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<ServiceResult<List<RoomDTO>>> GetAllRoomsAsync()
    {
        try
        {
            var rooms = await _context.Rooms
            .Include(t => t.Hotel)
            .Include(t => t.RoomImages)
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
                .Include(t => t.RoomImages)
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
            var hotel = await _context.Hotels.FindAsync(dto.HotelId);
            if (hotel == null)
                return ServiceResult<RoomDTO>.Failure("Hotel not found");

            var room = _mapper.Map<Room>(dto);
            room.Hotel = hotel;

            if (dto.ImageFiles != null && dto.ImageFiles.Any())
            {
                var uploadResult = await _imageService.UploadManyAsync(dto.ImageFiles, "rooms");

                if (!uploadResult.Success)
                    return ServiceResult<RoomDTO>.Failure(uploadResult.ErrorMessage!);

                room.RoomImages = uploadResult.Data
                    .Select(url => new RoomImage { ImageUrl = url, Room = room })
                    .ToList();
            }

            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();

            return new ServiceResult<RoomDTO>
            {
                Success = true,
                Data = _mapper.Map<RoomDTO>(room)
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<RoomDTO>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
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

            _mapper.Map(dto, room);

            if (dto.ImageFiles != null && dto.ImageFiles.Any())
            {
                var uploadResult = await _imageService.UploadManyAsync(dto.ImageFiles, "rooms");

                if (!uploadResult.Success)
                    return ServiceResult<RoomDTO>.Failure(uploadResult.ErrorMessage!);

                room.RoomImages = uploadResult.Data
                    .Select(url => new RoomImage { ImageUrl = url, Room = room })
                    .ToList();
            }

            hotel.Owner = owner;
            room.Hotel = hotel;

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
            var room = await _context.Rooms
                .Include(r => r.Reservations)
                .Include(r => r.RoomImages)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
            {
                return new ServiceResult<bool> { Success = false, ErrorMessage = "Room not found." };
            }

            if (room.Reservations.Any())
                _context.Reservations.RemoveRange(room.Reservations);

            if (room.RoomImages.Any())
                _context.RoomImages.RemoveRange(room.RoomImages);

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return new ServiceResult<bool> { Success = true, Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResult<bool> { Success = false, ErrorMessage = ex.InnerException?.Message ?? ex.Message };
        }
    }


    public async Task<ServiceResult<List<RoomDTO>>> SearchRoomsAsync(SearchRoomsDTO dto)
    {
        try
        {
            var rooms = await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.Reservations)
                .Include(r => r.RoomImages)
                .Where(r =>
                    r.Hotel.Location.Contains(dto.location) &&
                    r.Capacity >= dto.guests &&
                    !r.Reservations.Any(res =>
                        DateOnly.FromDateTime(dto.checkIn) < res.CheckOutDate &&
                        DateOnly.FromDateTime(dto.checkOut) > res.CheckInDate
                    )
                )
                .ToListAsync();

            var mappedRooms = rooms.Select(r => _mapper.Map<RoomDTO>(r)).ToList();

            return new ServiceResult<List<RoomDTO>> { Success = true, Data = mappedRooms };
        }
        catch (Exception ex)
        {
            return ServiceResult<List<RoomDTO>>.Failure(ex.Message);
        }
    }

}
