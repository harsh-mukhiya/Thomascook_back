using Booking.Data.Repository;
using Booking.Models.DatabaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CruiseBookingController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public CruiseBookingController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("addNewCruise")]
        public async Task<IActionResult> AddCruiseDetails(CruiseBooking cruise)
        {
            // Get the user ID from the claims (assuming you're in an authenticated context)
            /*var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;*/

            if (ModelState.IsValid)
            {
                string response = await _userRepository.AddNewCruise(cruise);
                return Ok(response);
            }
            return BadRequest("Data invalid");
        }
    }
}
