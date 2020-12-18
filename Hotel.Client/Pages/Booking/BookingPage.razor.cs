using Hotel.Client.Shared.Models.Info;
using Hotel.Client.Shared.Models.Request;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
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
        [Inject] NavigationManager NavigationManager { get; set; }
        BookingInfo ConfirmedBooking { get; set; }

        public async Task CreateBooking()
        {
            BookingRequest.BookingNumber = Guid.NewGuid().ToString();
            var result = await Http.PostAsJsonAsync("/booking", BookingRequest);
            if (result.IsSuccessStatusCode)
            {
                ConfirmedBooking = await Http.GetFromJsonAsync<BookingInfo>($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{BookingRequest.BookingNumber}");
            }


        }       
    }
}
