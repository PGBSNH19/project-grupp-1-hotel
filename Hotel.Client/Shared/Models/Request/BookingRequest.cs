using System;
namespace Hotel.Client.Shared.Models.Request
{
    public class BookingRequest 
    {
        public string BookingNumber { get; set; }
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
        public int DoubleBeds { get; set; }
        public bool IsCondo { get; set; }
        public bool IsSuite { get; set; }
    }
}