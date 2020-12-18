using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Server.Models.Request
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
        [StringLength(20, ErrorMessage = "First Name cannot be longer than 20 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Last Name cannot be longer than 20 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Phone Number cannot be longer than 20 characters")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Address cannot be longer than 30 characters")]
        public string Address { get; set; }
        [Required]
        [Range(0, 4)]
        public int Guests { get; set; }
        [Required]
        public bool Breakfast { get; set; } = false;
        public bool SpaAccess { get; set; } = false;
        #endregion

        #region Room specifications
        [Required]
        public int Beds { get; set; }
        [Required]
        public int DoubleBeds { get; set; }
        public bool IsCondo { get; set; }
        public bool IsSuite { get; set; }
        #endregion
    }
}