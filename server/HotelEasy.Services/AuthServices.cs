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
using AutoMapper;

namespace HotelEasy.Services
{
    public class AuthServices
    {
        private readonly Diplomna21180105Context _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthServices(Diplomna21180105Context context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ServiceResult<string>> RegisterUserAsync(UserCredentialsDTO registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                return ServiceResult<string>.Failure("Email already exists");

            var user = new User
            {
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);
            return ServiceResult<string>.SuccessResult(token);
        }

        public async Task<ServiceResult<string>> LoginAsync(UserCredentialsDTO UserLoginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == UserLoginDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(UserLoginDTO.Password, user.PasswordHash))
                return ServiceResult<string>.Failure("Invalid Email or password");

            var token = GenerateJwtToken(user);
            return ServiceResult<string>.SuccessResult(token);
        }

        public async Task<ServiceResult<List<UserDTO>>> GetAllUserAsync()
        {
            var users = await _context.Users
            .ToListAsync();
            return new ServiceResult<List<UserDTO>> { Success = true, Data = users.Select(t => _mapper.Map<UserDTO>(t)).ToList() };
        }

        public async Task<ServiceResult<UserDTO>> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(t => t.UserId == id);

                if (user == null)
                {
                    return new ServiceResult<UserDTO> { Success = false, ErrorMessage = "User not found." };
                }

                return new ServiceResult<UserDTO> { Success = true, Data = _mapper.Map<UserDTO>(user) };
            }
            catch (Exception ex)
            {
                return new ServiceResult<UserDTO> { Success = false, ErrorMessage = ex.Message };
            }
        }
        public async Task<ServiceResult<UserDTO>> UpdateUserAsync(int id, UserDTO dto)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return new ServiceResult<UserDTO> { Success = false, ErrorMessage = "User not found." };
                }

                _mapper.Map(dto, user);
                await _context.SaveChangesAsync();
                return new ServiceResult<UserDTO> { Success = true, Data = _mapper.Map<UserDTO>(user) };
            }
            catch (Exception ex)
            {
                return new ServiceResult<UserDTO> { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<ServiceResult<bool>> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return new ServiceResult<bool> { Success = false, ErrorMessage = "User not found." };
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return new ServiceResult<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool> { Success = false, ErrorMessage = ex.Message };
            }
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