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
        [Inject] AppState AppState { get; set; }
        [Parameter] public EventCallback OnValidSubmit { get; set; }

    }
}
