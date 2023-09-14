using Booking.Data.Repository;
using Booking.Models.DatabaseModels;
using Booking.Models.DTOModels;
using Booking.Services.AuthService.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Services.AuthService.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<User> Authenticate(LoginDTO data)
        {
            List<User> users = await _userRepository.GetAllUsers();
            User authenticatedUser = users.FirstOrDefault(u => u.Email == data.Email && u.Password == data.Password);
            return authenticatedUser;
        }

        public async Task<string> Register(SignUpDTO userDTO)
        {
            try
            {
                // Convert SignUpDTO to User
                User user = new User
                {
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                    Phone = userDTO.Phone
                    // You can set other properties here if needed
                };

                // Call the CreateUser method from your repository
                string userId = await _userRepository.CreateUser(user);

                // Return the user's ID
                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to register user", ex);
            }
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                    // You can add more claims here as needed
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audiance"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
