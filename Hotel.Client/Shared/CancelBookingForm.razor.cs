using Hotel.Shared;
using Hotel.Client.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
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
