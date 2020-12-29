using Hotel.Client.Toast;
using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hotel.Client.Pages.Booking
{
    public partial class Cancelation
    {
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Configuration { get; set; }
        [Inject] ToastService Toast { get; set; }
        [Inject] NavigationManager Nav { get; set; }

        [Parameter]
        public string bookingNumber { get; set; }
        public BookingInfo bookinginfo { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Nav.LocationChanged += HandleLocationChanged;
            if (!String.IsNullOrEmpty(bookingNumber))
            {
                Toast.ShowToast("Booking Number does not exist, please enter a booking number ", ToastLevel.Error);
                Console.WriteLine("Toasting");
            }
            else
            {
                await LoadBooking();
                StateHasChanged();

            }
        }

        public async Task LoadBooking()
        {
            StateHasChanged();
            bookinginfo = null;
            bookinginfo = await Http.GetFromJsonAsync<BookingInfo>($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{bookingNumber}");
            StateHasChanged();
            Console.WriteLine(bookingNumber);
            bookingNumber = null;
        }

        private async void HandleLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (Nav.ToBaseRelativePath(Nav.Uri).StartsWith("booking/cancel"))
            {
                await LoadBooking();
                StateHasChanged();
            }
        }
    }
}
