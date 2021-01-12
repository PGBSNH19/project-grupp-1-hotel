using Hotel.Client.ViewModel;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Hotel.Client.Shared
{
    public partial class CancelBookingForm
    {
        [Parameter] public CancelBookingViewModel CancelBookingRequest { get; set; } = new CancelBookingViewModel();

        [Parameter] public EventCallback<string> OnClick { get; set; }

        async Task OnClickInternal()
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(CancelBookingRequest.BookingNumber);
            }
        }
    }
}
