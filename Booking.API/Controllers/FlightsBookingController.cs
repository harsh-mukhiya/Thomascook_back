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
    public class FlightsBookingController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public FlightsBookingController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("userFlightDetails")]
        public async Task<ActionResult<List<FlightBooking>>> GetUserFlightBookings()
        {
            try
            {
                // Get the user ID from the claims (assuming you're in an authenticated context)
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
                User user = await _userRepository.GetUserById(userIdClaim.Value);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                List<FlightBooking> userBookings = user.Bookings ?? new List<FlightBooking>();
                return Ok(userBookings);
            }
            catch (Exception ex)
            {
                // Handle exceptions here, you can log or return an appropriate error response
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("addNewFlight")]
        public async Task<IActionResult> AddFlightDetails(FlightBooking flightDetails)
        {
            // Get the user ID from the claims (assuming you're in an authenticated context)
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;

            if(ModelState.IsValid)
            {
                string response = await _userRepository.AddNewFlight(flightDetails);
                return Ok(response);
            }
            return BadRequest("Data invalid");            
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("userAddFlightBooking")]
        public async Task<IActionResult> AddFlightBooking(string id,string flightId)
        {
            // Get the user ID from the claims (assuming you're in an authenticated context)
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            string updatedFlightId = "";
            if (int.TryParse(flightId, out int inputValue))
            {
                updatedFlightId = (inputValue - 1).ToString();
                /*Console.WriteLine(updatedFlightId);*/
            }
            else
            {
                return BadRequest("Flight Does not exist");
            }
            if (_userRepository.CheckFlightAvailability(updatedFlightId) == false)
            {
                return BadRequest("Flight Does not exist");
            }
            if (ModelState.IsValid)
            {
                var flightDetails = await _userRepository.GetFlightById(updatedFlightId);
                string response = await _userRepository.AddBooking(flightDetails,id);
                return Ok(response);
            }
            return BadRequest("Data invalid");
        }
        

    }
}