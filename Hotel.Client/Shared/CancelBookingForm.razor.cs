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
        [Parameter] public EventCallback<string> EventCallback { get; set; }
        [Inject] AppState AppState { get; set; }
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Configuration { get; set; }
        [Inject] NavigationManager Nav { get; set; }
        private BookingInfo BookingInfo { get; set; }
        async Task GetBooking()
        {
                BookingInfo = await Http.GetFromJsonAsync<BookingInfo>
                     ($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{BookingNumber}");
                if (BookingInfo != null)
                {
                    AppState.SetCancelBooking(BookingInfo);
                    Nav.NavigateTo($"cancel/{BookingNumber}");
                }
                else
                {
                    // todo: toast notification
                }
        }
    }
}
