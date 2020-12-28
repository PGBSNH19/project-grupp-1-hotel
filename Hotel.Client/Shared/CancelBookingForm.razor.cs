using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hotel.Client.Shared
{
    public partial class CancelBookingForm
    {
        public string BookingNumber { get; set; }
        [Parameter] public EventCallback EventCallback { get; set; }
        void GetBooking(string bookingNumber)
        {
            
        }
    }
}
