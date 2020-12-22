using Hotel.Shared;

namespace Hotel.Client.ViewModel
{
    public class RoomViewModel
    {
        public RoomInfo RoomInfo { get; set; }
        public RoomType RoomType => (RoomInfo.Beds, RoomInfo.DoubleBeds) switch
        {
            (1, 0) => RoomType.SingleBed,
            (1, 1) => RoomType.DoubleBeds,
            (2, 2) => RoomType.MasterSuite,
            (2, 1) => RoomType.TripleBeds,
            (2, 0) => RoomType.TwinBeds,
            _ => RoomType.Unknown
        };
    }

    public enum RoomType
    {
        SingleBed,
        DoubleBeds,
        TripleBeds,
        TwinBeds,
        MasterSuite,
        Unknown
    }
}
