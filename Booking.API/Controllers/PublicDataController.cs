using Booking.Data.Repository;
using Booking.Models.DatabaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicDataController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public PublicDataController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
       /* [HttpGet("allFlightDetails")]
        public async Task<ActionResult<List<FlightBooking>>> GetAllFlightDetails()
        {
            try
            {
                List<FlightBooking> allFlights=await _userRepository.GetAllFlightDetails();
                return Ok(allFlights);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }
*/
        [HttpGet("filteredFlightDetails")]
        public async Task<ActionResult<List<FlightBooking>>> FilteredFlightDetails(string booking_type, string departure_city, string arrival_city, string date)
        {
            try
            {
                List<FlightBooking> allFlights = await _userRepository.FilterFlightData(booking_type, departure_city, arrival_city, date);
                return Ok(allFlights);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }
      

        [HttpGet("specificFlightDetails")]
        public async Task<ActionResult<FlightBooking>> SpecificFlightDetails(string flightId)
        {
            try
            {
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
                var flightDetails = await _userRepository.GetFlightById(updatedFlightId);
                if(flightDetails == null)
                {
                    return BadRequest("No such Flight exist");
                }
                return Ok(flightDetails);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }

        }
        [HttpGet("allHotelDetails")]
        public async Task<ActionResult<List<HotelBooking>>> GetAllHotelDetails()
        {
            try
            {
                List<HotelBooking> allHotels = await _userRepository.GetAllHotelDetails();
                return Ok(allHotels);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpGet("specificHotelDetails")]
        public async Task<ActionResult<HotelBooking>> SpecificHotelDetails(string hotelId)
        {
            try
            {
                string updatedHotelId = "";
                if (int.TryParse(hotelId, out int inputValue))
                {
                    updatedHotelId = (inputValue - 1).ToString();
                    /*Console.WriteLine(updatedFlightId);*/
                }
                else
                {
                    return BadRequest("Flight Does not exist");
                }
                var hotelDetails = await _userRepository.GetHotelById(updatedHotelId);
                if (hotelDetails == null)
                {
                    return BadRequest("No such Hotel exist");
                }
                return Ok(hotelDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }

        }
        [HttpGet("filteredHotelDetails")]
        public async Task<ActionResult<List<HotelBooking>>> FilteredHotelDetails(string city)
        {
            try
            {
                List<HotelBooking> allHotel= await _userRepository.FilteredHotelDetails(city);
                return Ok(allHotel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }


        [HttpGet("filteredCruiseDetails")]
        public async Task<ActionResult<List<CruiseBooking>>> FilteredCruiseDetails(string departure_city, string sail_month, string sail_nights)
        {
            try
            {
                List<CruiseBooking> allCruise = await _userRepository.FilteredCruiseData(departure_city, sail_month, sail_nights);
                return Ok(allCruise);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }
        [HttpGet("specificCruiseDetails")]
        public async Task<ActionResult<CruiseBooking>> SpecificCruiseDetails(string Id)
        {
            try
            {
                string updatedCruiseId = "";
                if (int.TryParse(Id, out int inputValue))
                {
                    updatedCruiseId = (inputValue - 1).ToString();
                    /*Console.WriteLine(updatedFlightId);*/
                }
                else
                {
                    return BadRequest("Flight Does not exist");
                }
                var cruiseDetails = await _userRepository.GetCruiseById(updatedCruiseId);
                if (cruiseDetails == null)
                {
                    return BadRequest("No such Flight exist");
                }
                return Ok(cruiseDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }

        }
    }
}
