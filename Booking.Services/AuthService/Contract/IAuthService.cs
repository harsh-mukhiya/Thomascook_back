using Booking.Models.DatabaseModels;
using Booking.Models.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Services.AuthService.Contract
{
    public interface IAuthService
    {
        Task<User> Authenticate(LoginDTO data);
        string GenerateJwtToken(User user);
        Task<string> Register(SignUpDTO userDTO);
    }
}
