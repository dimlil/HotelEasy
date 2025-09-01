using AutoMapper;
using HotelEasy.Data;
using HotelEasy.Entities;
using HotelEasy.Services.Common;
using HotelEasy.Services.DTO;
using Microsoft.EntityFrameworkCore;

namespace HotelEasy.Services;

public class HotelsServices
{
    private readonly Diplomna21180105Context _context;
    private readonly IMapper _mapper;

    private readonly CloudinaryImageService _imageService;

    public HotelsServices(Diplomna21180105Context context, IMapper mapper, CloudinaryImageService imageService)
    {
        _context = context;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<ServiceResult<List<HotelDTO>>> GetAllHotelsAsync()
    {
        try
        {
            var hotel = await _context.Hotels
            .Include(t => t.Owner)
            .Include(t => t.HotelImages)
            .ToListAsync();
            return new ServiceResult<List<HotelDTO>> { Success = true, Data = hotel.Select(t => _mapper.Map<HotelDTO>(t)).ToList() };
        }
        catch (Exception ex)
        {
            return new ServiceResult<List<HotelDTO>> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<HotelDetailsDTO>> GetHotelByIdAsync(int id)
    {
        try
        {
            var hotel = await _context.Hotels
                .Include(t => t.Owner)
                .Include(t => t.HotelImages)
                .Include(t => t.Rooms)
                .FirstOrDefaultAsync(t => t.HotelId == id);

            if (hotel == null)
            {
                return new ServiceResult<HotelDetailsDTO> { Success = false, ErrorMessage = "Hotel not found." };
            }

            return new ServiceResult<HotelDetailsDTO> { Success = true, Data = _mapper.Map<HotelDetailsDTO>(hotel) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<HotelDetailsDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<HotelDTO>> CreateHotelAsync(CreateHotelDTO dto)
    {
        try
        {
            var hotel = _mapper.Map<Hotel>(dto);
            var owner = await _context.Users.FindAsync(dto.OwnerId);

            if (owner == null)
                return ServiceResult<HotelDTO>.Failure("User not found");

            if (owner.Role != "Owner")
            {
                return ServiceResult<HotelDTO>.Failure("User is not an owner");
            }

            if (dto.ImageFiles != null && dto.ImageFiles.Any())
            {
                var uploadResult = await _imageService.UploadManyAsync(dto.ImageFiles, "hotels");

                if (!uploadResult.Success)
                    return ServiceResult<HotelDTO>.Failure(uploadResult.ErrorMessage!);

                hotel.HotelImages = uploadResult.Data
                    .Select(url => new HotelImage { ImageUrl = url, Hotel = hotel })
                    .ToList();
            }


            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();
            // return new ServiceResult<CreateHotelDTO> { Success = true, Data = _mapper.Map<CreateHotelDTO>(hotel) };
            return new ServiceResult<HotelDTO>
            {
                Success = true,
                Data = _mapper.Map<HotelDTO>(hotel)
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<HotelDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<HotelDTO>> UpdateHitelAsync(int id, CreateHotelDTO dto)
    {
        try
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return new ServiceResult<HotelDTO> { Success = false, ErrorMessage = "Hotel not found." };
            }

            _mapper.Map(dto, hotel);
            await _context.SaveChangesAsync();
            return new ServiceResult<HotelDTO> { Success = true, Data = _mapper.Map<HotelDTO>(hotel) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<HotelDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<bool>> DeleteHotelAsync(int id)
    {
        try
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return new ServiceResult<bool> { Success = false, ErrorMessage = "Hotel not found." };
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return new ServiceResult<bool> { Success = true, Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResult<bool> { Success = false, ErrorMessage = ex.Message };
        }
    }
}
