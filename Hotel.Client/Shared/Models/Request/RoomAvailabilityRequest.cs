using System;
namespace Hotel.Shared.Models.Request
{
    public class RoomAvailabilityRequest
    {
        public int Guests { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}