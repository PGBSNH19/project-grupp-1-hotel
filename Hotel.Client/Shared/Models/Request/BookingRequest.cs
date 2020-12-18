using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Shared.Models.Request
{
    public class BookingRequest
    {
        #region Booking specifications
        [Required]
        public string BookingNumber { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Range(0, 4)]
        public int Guests { get; set; }
        [Required]
        public bool Breakfast { get; set; } = false;
        public bool SpaAccess { get; set; } = false;
        #endregion

        #region Room specifications
        public int Beds { get; set; }
        public int DoubleBeds { get; set; }
        public bool IsCondo { get; set; }
        public bool IsSuite { get; set; }
        #endregion
    }
}