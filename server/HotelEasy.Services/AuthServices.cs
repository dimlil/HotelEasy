using HotelEasy.Data;
using HotelEasy.Services.DTO;
using Microsoft.EntityFrameworkCore;
using HotelEasy.Services.Common;

namespace HotelEasy.Services
{
    public class AuthServices
    {
        private readonly Diplomna21180105Context _context;

        public AuthServices(Diplomna21180105Context context)
        {
            _context = context;
        }

        public async Task<ServiceResult<string>> LoginAsync(UserLoginDTO UserLoginDTO)
        {
            System.Console.WriteLine("Attempting to login user: " + UserLoginDTO.Email);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == UserLoginDTO.Email);
            
            return ServiceResult<string>.SuccessResult(user.Email);
        }
    }
}