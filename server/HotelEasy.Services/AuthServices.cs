using HotelEasy.Data;
using HotelEasy.Services.DTO;
using Microsoft.EntityFrameworkCore;
using HotelEasy.Services.Common;
using HotelEasy.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using dotenv.net;

namespace HotelEasy.Services
{
    public class AuthServices
    {
        private readonly Diplomna21180105Context _context;
        private readonly IConfiguration _configuration;
        // private readonly string JWTKey;
        // private readonly string JWTExpireMinutes;


        // public AuthServices(string JWTKey, string JWTExpireMinutes)
        // {
        //     _context = new Diplomna21180105Context();
        //     _configuration = new ConfigurationBuilder().Build();
        //     this.JWTKey = JWTKey;
        //     this.JWTExpireMinutes = JWTExpireMinutes;
        // }
        public AuthServices(Diplomna21180105Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == UserLoginDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(UserLoginDTO.Password, user.PasswordHash))
                return ServiceResult<string>.Failure("Invalid username or password");

            var token = GenerateJwtToken(user);
            return ServiceResult<string>.SuccessResult(token);
        }

        private string GenerateJwtToken(User user)
        {
            DotEnv.Load();
            var JWTKey = Environment.GetEnvironmentVariable("JWTKey")
                ?? throw new InvalidOperationException("JWTKey env is missing!");
            System.Console.WriteLine(JWTKey);
            var JWTExpireMinutes = Environment.GetEnvironmentVariable("JWTExpireMinutes")
                ?? throw new InvalidOperationException("JWTExpireMinutes env is missing!");
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(JWTExpireMinutes)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}