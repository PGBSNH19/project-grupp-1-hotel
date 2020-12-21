using Hotel.Client.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Client.Shared.Models.Request
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