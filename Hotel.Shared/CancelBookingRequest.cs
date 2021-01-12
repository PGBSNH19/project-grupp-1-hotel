using System.ComponentModel.DataAnnotations;

namespace Hotel.Client.Shared
{
    public class CancelBookingRequest
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]{8}-([A-Za-z0-9]{4}-){3}[A-Za-z0-9]{12}$", ErrorMessage = "BookingNumber must be correct format.")]
        public string BookingNumber { get; set; }
    }
}
