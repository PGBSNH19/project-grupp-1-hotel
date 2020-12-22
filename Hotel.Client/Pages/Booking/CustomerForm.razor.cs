using Hotel.Client.Shared;
using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hotel.Client.Pages.Booking
{
    public partial class CustomerForm
    {
        BookingRequest BookingRqs = new BookingRequest();
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Configuration { get; set; }
        [Inject] AppState AppState { get; set; }
        public BookingInfo ConfirmedBooking { get; set; }


        protected override void OnInitialized()
        {
            AppState.BookingRequest.Guests = AppState.AvailabilityRequest.Guests;
            AppState.BookingRequest.CheckInDate = AppState.AvailabilityRequest.CheckInDate;
            AppState.BookingRequest.CheckOutDate = AppState.AvailabilityRequest.CheckOutDate;          
        }

        public async Task CreateBooking()
        {
            try
            {
                AppState.BookingRequest.BookingNumber = Guid.NewGuid().ToString();
                var result = await Http.PostAsJsonAsync($"{Configuration["BaseApiUrl"]}api/v1.0/booking/", AppState.BookingRequest);
                if (result.IsSuccessStatusCode)
                {
                    ConfirmedBooking = await Http.GetFromJsonAsync<BookingInfo>($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{AppState.BookingRequest.BookingNumber}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
