using Hotel.Client.Shared.Models.Info;
using Hotel.Client.Shared.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Client.Shared
{
    public class AppState
    {
        public event Action OnChange;
        public RoomInfo[] Rooms { get; private set; }
        public RoomAvailabilityRequest AvailabilityRequest { get; set; }
        public void SetRooms(RoomInfo[] rooms)
        {
            Rooms = rooms;
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
