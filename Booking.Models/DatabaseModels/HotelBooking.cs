using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DatabaseModels
{
    public class HotelBooking
    {
        public string? Id { get; set; }
        public string? HotelId { get; set; }
        public string? HotelName { get; set; }
        public string? HotelDescription { get; set; }
        public string? City { get; set; }
        public string? CheckInDate { get; set; }
        public string? NumberOfRooms { get; set; }
        public string? Rating { get; set; }
        public string? HotelImageUrl { get; set; }
        public string? NormalRoomPrice { get; set; }
        public string? DeluxeRoomPrice { get; set; }



    }
}
