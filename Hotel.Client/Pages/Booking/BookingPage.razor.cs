using Hotel.Server.Models.Info;
using Hotel.Server.Models.Request;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace Hotel.Client.Pages.Booking
{
    public partial class BookingPage
    {
        BookingRequest BookingRequest = new BookingRequest();
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Configuration { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }
        BookingInfo ConfirmedBooking { get; set; }

        public async Task CreateBooking()
        {
            BookingRequest.BookingNumber = Guid.NewGuid().ToString();
            var result = await Http.PostAsJsonAsync("/booking", BookingRequest);

            ConfirmedBooking = await Http.GetFromJsonAsync<BookingInfo>($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{BookingRequest.BookingNumber}");

        }       
    }
}
