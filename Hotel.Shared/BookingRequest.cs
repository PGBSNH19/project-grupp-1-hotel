using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Shared
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
        [MaxLength(20, ErrorMessage = "Must have a length between 2 and 20."), MinLength(2, ErrorMessage = "Must have a length between 2 and 20.")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Must have a length between 2 and 20."), MinLength(2, ErrorMessage = "Must have a length between 2 and 20.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [RegularExpression(@"^([a-zA-Z.0-9]{5,20})(@[a-zA-Z.]{2,20})([a-zA-Z]{2,6}$)", ErrorMessage = "Invalid Email Address")]
        [MaxLength(50, ErrorMessage = "Must have a length between 7 and 50."), MinLength(10, ErrorMessage = "Must have a length between 10 and 50.")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(\+)?([0-9]{7,15})$", ErrorMessage = "Phone number not valid. Must have length between 7 and 15 and may only contain digits and plus sign.")]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Must have a length between 4 and 30."), MinLength(4, ErrorMessage = "Must have a length between 4 and 30.")]
        public string Address { get; set; }
        [Required]
        [Range(1, 4, ErrorMessage = "Allowed number of guests are between 1 and 4.")]
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