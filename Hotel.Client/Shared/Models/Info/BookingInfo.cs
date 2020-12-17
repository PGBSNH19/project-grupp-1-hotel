using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Models.Info
{
    public class BookingInfo
    {
        public int Id { get; set; }
        public string BookingNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public string Email { get; set; }
        public int Guests { get; set; }
        public bool Breakfast { get; set; }
        public bool SpaAccess { get; set; }
    }
}