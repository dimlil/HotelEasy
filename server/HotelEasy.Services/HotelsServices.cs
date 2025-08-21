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

    public HotelsServices(Diplomna21180105Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<HotelDTO>>> GetAllHotelsAsync()
    {
        try
        {
            var hotel = await _context.Hotels
            .Include(t => t.Owner)
            .ToListAsync();
            return new ServiceResult<List<HotelDTO>> { Success = true, Data = hotel.Select(t => _mapper.Map<HotelDTO>(t)).ToList() };
        }
        catch (Exception ex)
        {
            return new ServiceResult<List<HotelDTO>> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<HotelDTO>> GetHotelByIdAsync(int id)
    {
        try
        {
            var hotel = await _context.Hotels
                .Include(t => t.Owner)
                .FirstOrDefaultAsync(t => t.HotelId == id);

            if (hotel == null)
            {
                return new ServiceResult<HotelDTO> { Success = false, ErrorMessage = "Hotel not found." };
            }

            return new ServiceResult<HotelDTO> { Success = true, Data = _mapper.Map<HotelDTO>(hotel) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<HotelDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<HotelDTO>> CreateHotelAsync(HotelDTO dto)
    {
        try
        {
            var hotel = _mapper.Map<Hotel>(dto);
            var owner = await _context.Users.FindAsync(dto.OwnerId);

            if (owner == null)
                return ServiceResult<HotelDTO>.Failure("User not found");

            hotel.Owner = owner;

            await _context.Hotels.AddAsync(hotel);
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
