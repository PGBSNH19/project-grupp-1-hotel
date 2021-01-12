using System.ComponentModel.DataAnnotations;

namespace Hotel.Client.ViewModel
{
    public class CancelBookingViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]{8}-([A-Za-z0-9]{4}-){3}[A-Za-z0-9]{12}$", ErrorMessage = "BookingNumber must be correct format.")]
        public string BookingNumber { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [RegularExpression(@"^([a-zA-Z.0-9]{1,55})(@[a-zA-Z.0-9-]{1,255})[.]([a-zA-Z]{2,6})$", ErrorMessage = "Invalid email address")]
        [MaxLength(260, ErrorMessage = "Email address have a bad length."), MinLength(5, ErrorMessage = "Email address have a bad length.")]
        public string Email { get; set; }

    }
}
