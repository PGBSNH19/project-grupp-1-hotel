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
        [MaxLength(64, ErrorMessage = "Bookingnumber is too long"), MinLength(24, ErrorMessage = "Bookingnumber is too short")]
        public string BookingNumber { get; set; }
    }
}
