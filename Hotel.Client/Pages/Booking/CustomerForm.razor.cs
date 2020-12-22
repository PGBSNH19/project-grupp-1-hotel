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
        //[Parameter] public BookingRequest BookingRequest { get; set; } = new BookingRequest();
        //[Parameter] public EventCallback OnValidSubmit { get; set; }

        BookingRequest BookingRqs = new BookingRequest();
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Configuration { get; set; }

        public BookingInfo ConfirmedBooking { get; set; }


        public async Task CreateBooking()
        {
            Console.WriteLine(BookingRqs.PhoneNumber);
            try
            {
                BookingRqs.BookingNumber = Guid.NewGuid().ToString();
                var result = await Http.PostAsJsonAsync($"{Configuration["BaseApiUrl"]}api/v1.0/booking/", BookingRqs);
                if (result.IsSuccessStatusCode)
                {
                    ConfirmedBooking = await Http.GetFromJsonAsync<BookingInfo>($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{BookingRqs.BookingNumber}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
