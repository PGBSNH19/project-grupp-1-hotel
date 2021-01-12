using System.ComponentModel.DataAnnotations;

namespace Hotel.Client.ViewModel
{
    public class CancelBookingViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]{8}-([A-Za-z0-9]{4}-){3}[A-Za-z0-9]{12}$", ErrorMessage = "BookingNumber must be correct format.")]
        public string BookingNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(25), MinLength(6)]
        public string Email { get; set; }

    }
}
