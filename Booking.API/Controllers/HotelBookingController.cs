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
    public class HotelBookingController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public HotelBookingController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("addNewHotel")]
        public async Task<IActionResult> AddHotelDetails(HotelBooking hotelDetails)
        {
            // Get the user ID from the claims (assuming you're in an authenticated context)
           

            if (ModelState.IsValid)
            {
                string response = await _userRepository.AddNewHotel(hotelDetails);
                return Ok(response);
            }
            return BadRequest("Data invalid");
        }


    }
}
