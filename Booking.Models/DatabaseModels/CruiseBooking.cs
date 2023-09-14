using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DatabaseModels
{
    public class CruiseBooking
    {
        public string? Id { get; set; }
        public string CruiseId { get; set; }
        public string CruiseName { get; set; }
        public string DepartureCity { get; set; }
        public string SailMonth { get; set; }
        public string SailNights { get; set; }
        public string DestinationCity { get; set; }
        public string DepartingDate { get; set; }
        public string DepartingTime { get; set; }
        public string ArrivalDate { get; set; }
        public string ArrivalTime { get; set; }
        public string OceanviewPrice { get; set; }
        public string BalconyPrice { get; set; }
        public string SuitePrice { get; set; }
        public string TotalPassengers { get; set; }
        public string CruiseImageUrl{ get; set; }
    }
}
