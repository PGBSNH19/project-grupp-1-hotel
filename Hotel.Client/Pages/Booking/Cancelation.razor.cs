using Hotel.Client.Shared;
using Hotel.Client.Toast;
using Hotel.Client.ViewModel;
using Hotel.Shared;
using Microsoft.AspNetCore.Components;
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
        public RoomViewModel RoomViewModel { get; set; }
        public CancelBookingViewModel CancelBookingRequest { get; set; } = new CancelBookingViewModel();

        [Parameter]
        public string BookingNumber { get; set; }
        public BookingInfo Bookinginfo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CancelBookingRequest.BookingNumber = BookingNumber;
            if (String.IsNullOrEmpty(CancelBookingRequest.BookingNumber))
            {
                Toast.ShowToast("Please enter a booking number ", ToastLevel.Warning);
                Nav.NavigateTo("/");
            }
            else
            {
                await LoadBooking();
                StateHasChanged();
            }
        }

        public async Task LoadBooking()
        {
            Bookinginfo = null;

            try
            {
                Bookinginfo = await Http.GetFromJsonAsync<BookingInfo>($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{CancelBookingRequest.BookingNumber}");
                Console.WriteLine(Bookinginfo.IsCanceled);
                RoomViewModel = new RoomViewModel { RoomInfo = Bookinginfo.Room };
            }
            catch (Exception ex)
            {
                Toast.ShowToast("Your booking number doesn't exist!", ToastLevel.Warning);
                Nav.NavigateTo("/");
                Console.WriteLine(ex.StackTrace);
            }
        }

        public async Task CancelBooking()
        {
            if (!String.IsNullOrEmpty(CancelBookingRequest.Email))
            {
                var result = await Http.PutAsJsonAsync($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{CancelBookingRequest.BookingNumber}/cancel", CancelBookingRequest.Email.ToLower());
                if (result.IsSuccessStatusCode)
                {
                    Toast.ShowToast($"You have canceled your booking with the booking number {CancelBookingRequest.BookingNumber}", ToastLevel.Success);
                    CancelBookingRequest = new CancelBookingViewModel();
                    StateHasChanged();
                }
                else
                {
                    Toast.ShowToast("Cant find your email, please enter your correct email", ToastLevel.Error);
                }
            }
            else
            {
                Toast.ShowToast("Please enter your email", ToastLevel.Error);
            }
        }
    }
}
