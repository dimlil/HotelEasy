using HotelEasy.Data;
using HotelEasy.Services.DTO;
using Microsoft.EntityFrameworkCore;
using HotelEasy.Services.Common;
using HotelEasy.Entities;

namespace HotelEasy.Services
{
    public class AuthServices
    {
        private readonly Diplomna21180105Context _context;

        public AuthServices(Diplomna21180105Context context)
        {
            _context = context;
        }

        public async Task<ServiceResult<string>> RegisterUserAsync(UserRegisterDTO registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                return ServiceResult<string>.Failure("Username already exists");

            var user = new User
            {
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.PasswordHash),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return ServiceResult<string>.SuccessResult(user.Email);
        }

        public async Task<ServiceResult<string>> LoginAsync(UserLoginDTO UserLoginDTO)
        {
            System.Console.WriteLine("Attempting to login user: " + UserLoginDTO.Email);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == UserLoginDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(UserLoginDTO.Password, user.PasswordHash))
                return ServiceResult<string>.Failure("Invalid username or password");

            return ServiceResult<string>.SuccessResult(user.Email);
        }
    }
}