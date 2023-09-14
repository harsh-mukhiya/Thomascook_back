using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DatabaseModels
{
    public class FlightBooking
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Flight ID  is required")]
        public string? flightid { get; set; }
        [Required(ErrorMessage = "Flight Name  is required")]
        public string? flight_name { get; set; }
        [Required(ErrorMessage = "Booking Type is required")]
        public string? booking_type { get; set; }
        [Required(ErrorMessage = "Departure City is required")]
        public string? departure_city { get; set; }
        [Required(ErrorMessage = "Arrival City is required")]
        public string? arrival_city { get; set; }
        [Required(ErrorMessage = "Date is required")]
        public string? date { get; set; }
        [Required(ErrorMessage = "Passengers is required")]
        public string? total_passengers { get; set; }
        [Required(ErrorMessage = "Flight Image URL is required")]
        public string? flight_image_url { get; set; }
        [Required(ErrorMessage = "Cost is required")]
        public string? cost { get; set; }

        [Required(ErrorMessage = "Departure Time is required")]
        public string? departure_time { get; set; }
        [Required(ErrorMessage = "Arrival Time is required")]
        public string? arrival_time { get; set; }
        [Required(ErrorMessage = "Flight Duration is required")]
        public string? flight_duration { get; set; }

    }
}
