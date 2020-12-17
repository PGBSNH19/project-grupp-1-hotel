using Hotel.Server.Models.Info;
using Hotel.Server.Models.Request;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Client.Pages.Booking
{
    public partial class ConfirmationPage
    {
        [Parameter] public BookingRequest MyInformation { get; set; }
        [Parameter] public string MyName { get; set; }
        [Parameter] public string MyEmail { get; set; }
        
        public void SetFields()
        {
            MyInformation = new BookingRequest
            {
                Email = MyEmail,
                FirstName = MyName
            };
        }

    }
}
