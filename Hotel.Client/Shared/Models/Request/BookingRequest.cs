using System;
namespace Hotel.Shared.Models.Request
{
    public class BookingRequest 
    {
        public DateTime CheckInDate { get; set; } = DateTime.Now;
        public DateTime CheckOutDate { get; set; } = DateTime.Now.AddDays(1);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Guests { get; set; } = 1;
        public bool Breakfast { get; set; }
        public bool SpaAccess { get; set; }
        public int DoubleBeds { get; set; }
        public bool IsCondo { get; set; }
        public bool IsSuite { get; set; }
    }
}