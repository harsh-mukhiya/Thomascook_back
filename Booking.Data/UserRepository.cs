using Booking.Data.Repository;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Booking.Models.DatabaseModels;

namespace Booking.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IFirebaseConfig _config;
        private readonly IFirebaseClient _client;

        public UserRepository(IConfiguration iconfig)
        {
            _config = new FirebaseConfig
            {
                AuthSecret = iconfig["Firebase:AuthSecret"],
                BasePath = iconfig["Firebase:BasePath"]
            };
            _client = new FireSharp.FirebaseClient(_config);
        }
        public async Task<string> CreateUser(User user)
        {
            try
            {
                var data = user;
                PushResponse response = await _client.PushAsync("User/", data);
                data.Id = response.Result.name;
                await _client.SetAsync("User/" + data.Id, data);
                return data.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create user", ex);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                FirebaseResponse response = await _client.GetAsync("User");
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
                var list = new List<User>();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        list.Add(JsonConvert.DeserializeObject<User>(((JProperty)item).Value.ToString()));
                    }
                }
                return list;
            } catch (Exception ex)
            {
                throw new Exception("Failed to load user data", ex);
            }
            
        }
       
        public async Task<User> GetUserById(string id)
        {
            try
            {
                FirebaseResponse res = await _client.GetAsync("User/" + id);

                if (res != null && res.Body != null)
                {
                    User data = JsonConvert.DeserializeObject<User>(res.Body);
                    return data;
                }
                else
                {
                    // Handle the case where the user doesn't exist
                    return null; // You can customize this return value as needed
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null; 
            }
        }
       
        public async Task<string> AddBooking(FlightBooking booking,string id)
        {
            try
            {
                User user = await GetUserById(id);
                if (user == null)
                {
                    return "User not found";
                }

                // Initialize the user's bookings list if it's null
                if (user.Bookings == null)
                {
                    user.Bookings = new List<FlightBooking>();
                }

                // Add the new booking to the user's bookings
                user.Bookings.Add(booking);
                await _client.SetAsync("User/" + id, user);
                return "New Booking Added!";
            } 
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return "An error occured";
            }
            
        }

        public async Task<string> AddNewFlight(FlightBooking flight)
        {
            try
            {
                var data = flight;
                PushResponse response = await _client.PushAsync("Flight/", data);
                data.Id = response.Result.name;
                await _client.SetAsync("Flight/" + data.Id, data);
                return data.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add Flight Details", ex);
            }
        }
        public async Task<string> AddNewCruise(CruiseBooking cruise)
        {
            try
            {
                var data = cruise;
                PushResponse response = await _client.PushAsync("Cruise/", data);
                data.Id = response.Result.name;
                await _client.SetAsync("Cruise/" + data.Id, data);
                return data.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add Cruise Details", ex);
            }
        }
        public async Task<List<FlightBooking>> GetAllFlight()
        {
            try
            {
                FirebaseResponse response = await _client.GetAsync("Flight");
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
                var list = new List<FlightBooking>();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        list.Add(JsonConvert.DeserializeObject<FlightBooking>(((JProperty)item).Value.ToString()));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load flight data", ex);
            }
        }
        public async Task<List<FlightBooking>> FilterFlightData(string booking_type, string departure_city, string arrival_city, string date)
        {
            try
            {
                FirebaseResponse response = await _client.GetAsync("Flight");
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
                var filteredList = new List<FlightBooking>();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        var flightBooking = ((JObject)item).ToObject<FlightBooking>(); // Deserialize directly to FlightBooking

                        // Check if the properties match the filter criteria
                        if ((string.IsNullOrEmpty(booking_type) || flightBooking.booking_type == booking_type) &&
                            (string.IsNullOrEmpty(departure_city) || flightBooking.departure_city== departure_city) &&
                            (string.IsNullOrEmpty(arrival_city) || flightBooking.arrival_city == arrival_city) &&
                            (string.IsNullOrEmpty(date) || flightBooking.date == date))
                        {
                            filteredList.Add(flightBooking);
                        }
                    }
                }

                return filteredList;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load and filter flight data", ex);
            }
        }        


        public async Task<FlightBooking> GetFlightById(string id)
        {
            try
            {
                FirebaseResponse res = await _client.GetAsync("Flight/" + id);

                if (res != null && res.Body != null)
                {
                    FlightBooking data = JsonConvert.DeserializeObject<FlightBooking>(res.Body);
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }
        public bool CheckFlightAvailability(string flightId)
        {
            var flightDetails = GetFlightById(flightId);
            if (flightDetails == null)
            {
                return false;
            }
            return true;
        }
        public async Task<List<FlightBooking>> GetAllFlightDetails()
        {
            try
            {
                FirebaseResponse response = await _client.GetAsync("Flight");
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
                var list = new List<FlightBooking>();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        list.Add(JsonConvert.DeserializeObject<FlightBooking>(((JProperty)item).Value.ToString()));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load flight data", ex);
            }
        }

        public async Task<string> AddNewHotel(HotelBooking hotel)
        {
            try
            {
                var data = hotel;
                PushResponse response = await _client.PushAsync("Hotel/", data);
                data.Id = response.Result.name;
                await _client.SetAsync("Hotel/" + data.Id, data);
                return data.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add Hotel Details", ex);
            }
        }
        
        public async Task<HotelBooking> GetHotelById(string id)
        {
            try
            {
                FirebaseResponse res = await _client.GetAsync("Hotel/" + id);

                if (res != null && res.Body != null)
                {
                    HotelBooking data = JsonConvert.DeserializeObject<HotelBooking>(res.Body);
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

        public async Task<List<HotelBooking>> GetAllHotelDetails()
        {
            try
            {
                FirebaseResponse response = await _client.GetAsync("Hotel");
                string responseBody = response.Body;

                if (!string.IsNullOrEmpty(responseBody))
                {
                    dynamic data = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    var list = new List<HotelBooking>();

                    foreach (var item in data)
                    {
                        var hotelBooking = JsonConvert.DeserializeObject<HotelBooking>(item.ToString());
                        list.Add(hotelBooking);
                    }

                    return list;
                }
                else
                {
                    // Handle the case where the response is empty or null
                    return new List<HotelBooking>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load Hotel data", ex);
            }
        }

        public async Task<List<HotelBooking>> FilteredHotelDetails(string city)
        {
            try
            {
                FirebaseResponse response = await _client.GetAsync("Hotel");
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
                var filteredList = new List<HotelBooking>();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        var hotelBooking = ((JObject)item).ToObject<HotelBooking>(); // Deserialize directly to FlightBooking

                        // Check if the properties match the filter criteria
                        if (string.IsNullOrEmpty(city) || hotelBooking.City == city)   
                        {
                            filteredList.Add(hotelBooking);
                        }
                    }
                }

                return filteredList;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load and filter cruise data", ex);
            }
        }

        public async Task<List<CruiseBooking>> FilteredCruiseData(string departure_city, string sail_month, string sail_nights)
        {
            try
            {
                FirebaseResponse response = await _client.GetAsync("Cruise");
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
                var filteredList = new List<CruiseBooking>();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        var cruiseBooking = ((JObject)item).ToObject<CruiseBooking>(); // Deserialize directly to FlightBooking

                        // Check if the properties match the filter criteria
                        if ((string.IsNullOrEmpty(departure_city) || cruiseBooking.DepartureCity == departure_city) &&
                            (string.IsNullOrEmpty(sail_month) || cruiseBooking.SailMonth == sail_month) &&
                            (string.IsNullOrEmpty(sail_nights) || cruiseBooking.SailNights == sail_nights))
                        {
                            filteredList.Add(cruiseBooking);
                        }
                    }
                }

                return filteredList;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load and filter cruise data", ex);
            }
        }
        public async Task<CruiseBooking> GetCruiseById(string Id)
        {
            try
            {
                FirebaseResponse res = await _client.GetAsync("Cruise/" + Id);

                if (res != null && res.Body != null)
                {
                    CruiseBooking data = JsonConvert.DeserializeObject<CruiseBooking>(res.Body);
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }
    }
}
