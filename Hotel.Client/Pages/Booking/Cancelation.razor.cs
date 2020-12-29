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
            if (String.IsNullOrEmpty(bookingNumber))
            {
                Toast.ShowToast("Booking Number does not exist, please enter a booking number ", ToastLevel.Warning);
                Nav.NavigateTo("/");
            }
            else
            {
                await LoadBooking();
                StateHasChanged();
                bookingNumber = String.Empty;
            }
        }

        public async Task LoadBooking()
        {
            bookinginfo = null;

            try
            {
                bookinginfo = await Http.GetFromJsonAsync<BookingInfo>($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{bookingNumber}");
            }
            catch (Exception ex)
            {
                Toast.ShowToast("BLÄÄÄÄÄ", ToastLevel.Warning);
                Nav.NavigateTo("/");
                Console.WriteLine(ex.StackTrace);
            }
            Console.WriteLine(bookingNumber);
        }
    }
}
