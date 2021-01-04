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

        public string RoomTypeInfo => (RoomInfo.Beds, RoomInfo.DoubleBeds) switch
        {
            (1, 0) => "Single bed",
            (1, 1) => "Double bed",
            (2, 2) => "Master suite",
            (2, 1) => "Triple bed",
            (2, 0) => "Twin bed",
            _ => string.Empty
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
