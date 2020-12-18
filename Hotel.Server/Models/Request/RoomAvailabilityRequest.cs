using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Server.Models.Request
{
    public class RoomAvailabilityRequest
    {
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