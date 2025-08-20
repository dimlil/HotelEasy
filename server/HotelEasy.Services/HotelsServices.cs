using AutoMapper;
using HotelEasy.Data;
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
            var trips = await _context.Hotels
            .Include(t => t.Owner)
            .ToListAsync();
            return new ServiceResult<List<HotelDTO>> { Success = true, Data = trips.Select(t => _mapper.Map<HotelDTO>(t)).ToList() };
        }
        catch (Exception ex)
        {
            return new ServiceResult<List<HotelDTO>> { Success = false, ErrorMessage = ex.Message };
        }
    }
}
