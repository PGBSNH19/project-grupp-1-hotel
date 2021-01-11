using Hotel.Client.ViewModel;
using Hotel.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Client.Shared
{
    public class AppState
    {
        public event Action OnChange;
        public List<RoomViewModel> Rooms { get; private set; }
        public RoomAvailabilityRequest AvailabilityRequest { get; set; } = new RoomAvailabilityRequest();
        public BookingInfo ConfirmedBooking { get; set; }
        public RoomInfo PickedRoom { get; set; }
        public BookingRequest BookingRequest { get; set; } = new BookingRequest();

        public void SetPickedRoom(RoomInfo pickedRoom)
        {
            PickedRoom = pickedRoom;
            BookingRequest.Beds = pickedRoom.Beds;
            BookingRequest.DoubleBeds = pickedRoom.DoubleBeds;
            BookingRequest.BookingNumber = "iiiwww";
        }

        public void Flush()
        {
            Rooms = null;
            ConfirmedBooking = null;
            PickedRoom = null;
            BookingRequest = new BookingRequest();
            AvailabilityRequest = new RoomAvailabilityRequest();
            BookingRequest.BookingNumber = "<placeholder>";
        }

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
            ConfirmedBooking.Email.ToLower();
            NotifyStateChanged();
        }

        public void SetAvailabilityRequest(RoomAvailabilityRequest availabilityRequest)
        {
            AvailabilityRequest = availabilityRequest;
            NotifyStateChanged();
        }

        public void SetBookingRequest(RoomAvailabilityRequest availabilityRequest)
        {
            BookingRequest.CheckInDate = availabilityRequest.CheckInDate;
            BookingRequest.CheckOutDate = availabilityRequest.CheckOutDate;
            BookingRequest.Guests = availabilityRequest.Guests;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
