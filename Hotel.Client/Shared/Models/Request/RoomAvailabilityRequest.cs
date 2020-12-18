using System;
namespace Hotel.Shared.Models.Request
{
    public class RoomAvailabilityRequest
    {
        public int Guests { get; set; } = 1;
        public DateTime CheckInDate { get; set; } = DateTime.Now;
        public DateTime CheckOutDate { get; set; } = DateTime.Now.AddDays(1);
    }
}