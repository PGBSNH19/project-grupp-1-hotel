using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace Hotel.Client.Pages.Booking
{
    public partial class BookingPage
    {
        BookingRequest BookingRequest = new BookingRequest();
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Configuration { get; set; }

        BookingInfo ConfirmedBooking { get; set; }

        //public async Task CreateBooking()
        //{
        //    Console.WriteLine(BookingRequest.PhoneNumber);
        //    try
        //    {
        //        BookingRequest.BookingNumber = Guid.NewGuid().ToString();
        //        var result = await Http.PostAsJsonAsync($"{Configuration["BaseApiUrl"]}api/v1.0/booking/", BookingRequest);
        //        if (result.IsSuccessStatusCode)
        //        {
        //            ConfirmedBooking = await Http.GetFromJsonAsync<BookingInfo>($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{BookingRequest.BookingNumber}");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}
    }
}
