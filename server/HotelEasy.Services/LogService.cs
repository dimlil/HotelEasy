using AutoMapper;
using HotelEasy.Data;
using HotelEasy.Entities;
using HotelEasy.Services.Common;
using HotelEasy.Services.DTO;
using Microsoft.EntityFrameworkCore;

namespace HotelEasy.Services;

public class LogServices
{
    private readonly Diplomna21180105Context _context;

    public LogServices(Diplomna21180105Context context)
    {
        _context = context;
    }

    public async Task<ServiceResult<List<Log21180105>>> GetAllLogs()
    {
        try
        {
            var logs = await _context.Log21180105s
            .ToListAsync();
            return new ServiceResult<List<Log21180105>> { Success = true, Data = logs };
        }
        catch (Exception ex)
        {
            return new ServiceResult<List<Log21180105>> { Success = false, ErrorMessage = ex.Message };
        }
    }
}
