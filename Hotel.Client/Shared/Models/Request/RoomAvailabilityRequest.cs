using Hotel.Client.Shared;
using System;

namespace Hotel.Shared.Models.Request
{
    public class RoomAvailabilityRequest 
    {
        public int Guests { get; set; } = 1;
        public DateTime CheckInDate { get; set; } = DateTime.Now;
        public DateTime CheckOutDate { get; set; } = DateTime.Now.AddDays(1);

        [Required]
        [Range(1, 4)]
        public int Guests { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }
    }
}