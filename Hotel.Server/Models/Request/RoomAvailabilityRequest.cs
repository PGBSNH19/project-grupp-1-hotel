using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Server.Models.Request
{
    public class RoomAvailabilityRequest
    {
        [Required]
        [Range(0, 4)]
        public int Guests { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
    }
}