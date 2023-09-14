using Booking.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Repository
{
    public interface IUserRepository
    {
        Task<string> CreateUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task<string> AddBooking(FlightBooking booking, string id);
        Task<string> AddNewFlight(FlightBooking flight);
        Task<List<FlightBooking>> GetAllFlight();
        Task<FlightBooking> GetFlightById(string id);
        bool CheckFlightAvailability(string flightId);
        Task<List<FlightBooking>> GetAllFlightDetails();
        Task<List<FlightBooking>> FilterFlightData(string booking_type,string departure_city,string arrival_city,string date);
        Task<string> AddNewHotel(HotelBooking hotel);
        Task<HotelBooking> GetHotelById(string id);
        Task<List<HotelBooking>> FilteredHotelDetails(string city);
        Task<List<HotelBooking>> GetAllHotelDetails();
        Task<string> AddNewCruise(CruiseBooking cruise);
        Task<List<CruiseBooking>> FilteredCruiseData(string departure_city,string sail_month, string sail_nights);
        Task<CruiseBooking> GetCruiseById(string CruiseId);
    }
}
