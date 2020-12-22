using Hotel.Client.ViewModel;
using Hotel.Shared;
using System;
using System.Collections.Generic;

namespace Hotel.Client.Shared
{
    public class AppState
    {
        public event Action OnChange;
        public List<RoomViewModel> Rooms { get; private set; }
        public RoomAvailabilityRequest AvailabilityRequest { get; set; }
        public BookingInfo ConfirmedBooking { get; set; }

        public void SetRooms(RoomInfo[] rooms)
        {
            Rooms = new List<RoomViewModel>();
            foreach (var room in rooms)
                Rooms.Add(new RoomViewModel { RoomInfo = room });
            NotifyStateChanged();
        }

        public void SetConfirmedBooking(BookingInfo confirmedBooking)
        {
            ConfirmedBooking = confirmedBooking;
            NotifyStateChanged();
        }

        public void SetAvailabilityRequest(RoomAvailabilityRequest availabilityRequest)
        {
            AvailabilityRequest = availabilityRequest;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
