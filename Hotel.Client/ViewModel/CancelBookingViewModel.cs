using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Client.ViewModel
{
    public class CancelBookingViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]{8}-([A-Za-z0-9]{4}-){3}[A-Za-z0-9]{12}$", ErrorMessage = "BookingNumber must be correct format.")]
        public string BookingNumber { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(25), MinLength(6)]
        public string Email { get; set; }

    }
}
