namespace Booking.Models.DatabaseModels
{
    public class User
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public List<FlightBooking>? Bookings { get; set; }
    }
}
