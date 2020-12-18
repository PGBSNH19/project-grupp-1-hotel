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
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }
        [Required]
        [MaxLength(20), MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20), MinLength(2)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(25), MinLength(6)]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(\+)?([0-9]{6,17})$", ErrorMessage = "Phone number not valid. Must have length between 6 and 17 and may only contain digits and plus sign.")]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(30), MinLength(4)]
        public string Address { get; set; }
        [Required]
        [Range(1, 4)]
        public int Guests { get; set; }
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