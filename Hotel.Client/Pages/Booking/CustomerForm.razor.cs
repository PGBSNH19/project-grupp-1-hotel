using Hotel.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace Hotel.Client.Pages.Booking
{
    public partial class CustomerForm
    {
        [Inject] AppState AppState { get; set; }
        [Parameter] public EventCallback OnValidSubmit { get; set; }

    }
}
