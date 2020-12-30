using Hotel.Client.Shared;
using Hotel.Client.Toast;
using Hotel.Client.ViewModel;
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
        [Inject] AppState AppState { get; set; }
        public RoomViewModel roomViewModel { get; set; }

        [Parameter]
        public string bookingNumber { get; set; }
        public BookingInfo bookinginfo { get; set; }
        public string email { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (String.IsNullOrEmpty(bookingNumber))
            {
                Toast.ShowToast("Please enter a booking number ", ToastLevel.Warning);
                Nav.NavigateTo("/");
            }
            else
            {
                await LoadBooking();
                StateHasChanged();
                //bookingNumber = String.Empty;
            }
        }

        public async Task LoadBooking()
        {
            bookinginfo = null;

            try
            {
                bookinginfo = await Http.GetFromJsonAsync<BookingInfo>($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{bookingNumber}");
                Console.WriteLine(bookinginfo.IsCanceled);
                roomViewModel = new RoomViewModel { RoomInfo = bookinginfo.Room};
            }
            catch (Exception ex)
            {
                Toast.ShowToast("Your booking number doesn't exist!", ToastLevel.Warning);
                Nav.NavigateTo("/");
                Console.WriteLine(ex.StackTrace);
            }
        }

        public async Task DeleteBooking()
        {
            var result = await Http.PutAsJsonAsync($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{bookingNumber}/cancel",email);

            if(result.IsSuccessStatusCode)
            {
                Toast.ShowToast($"You have canceled you booking with the booking number {bookingNumber}", ToastLevel.Success);
            }
            else
            {
                Toast.ShowToast("Cant find your email", ToastLevel.Error);
                Nav.NavigateTo("/");
            }
        }
    }
}
