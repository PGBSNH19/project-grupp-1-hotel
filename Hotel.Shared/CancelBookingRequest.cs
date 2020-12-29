using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Client.Shared
{
    public class CancelBookingRequest
    {
        [Required]
        [MaxLength(64), MinLength(24)]
        public string BookingNumber { get; set; }
    }
}
