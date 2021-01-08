using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Shared
{
    public class RoomAvailabilityRequest
    {
        [Required]
        [Range(1, 4)]
        public int Guests { get; set; } = 1;
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; } = DateTime.Now;
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; } = DateTime.Now.AddDays(1);
    }
}