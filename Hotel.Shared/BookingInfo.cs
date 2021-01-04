using System;
namespace Hotel.Shared
{
    public class BookingInfo
    {
        public int Id { get; set; }
        public bool IsCanceled { get; set; }
        public string BookingNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public RoomInfo Room { get; set; }


        public string Email { get; set; }
        public int Guests { get; set; }
        public bool Breakfast { get; set; }
        public bool SpaAccess { get; set; }
    }
}