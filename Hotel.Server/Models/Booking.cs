using System;

namespace Hotel.Server.Models
{
    public class Booking
    {
	    public int Id { get; set; }
        public bool IsCanceled { get; private set; } = false;
        public string BookingNumber { get; set; }
        public Room Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Guests { get; set; }
        public bool Breakfast { get; set; }
        public bool SpaAccess { get; set; }
        public DateTime Created { get; private set; } = DateTime.Now;

        public void Cancel() => IsCanceled = true;
    }
}
