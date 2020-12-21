namespace Hotel.Client.Shared.Models.Info
{
    public class RoomInfo
    {
        public int Id { get; set; }
        public int Beds { get; set; }
        public int DoubleBeds { get; set; }
        public bool IsCondo { get; set; }
        public bool IsSuite { get; set; }
        public bool Smoking { get; set; }
        public bool Pets { get; set; }
        public string ImageUrl { get; set; }
        public RoomType RoomType { get; set; }

    }

    public enum RoomType
    {
        SingleBed,
        DoubleBeds,
        TripleBeds,
        TwinBeds,
        MasterSuite
    }

    public static class RoomExtension
    {
        public static RoomInfo RoomInfos(this RoomInfo room)
        {
            RoomInfo roomInfo = new RoomInfo();
            roomInfo.Id = room.Id;
            roomInfo.IsSuite = room.IsSuite;
            roomInfo.Pets = room.Pets;
            roomInfo.ImageUrl = room.ImageUrl;
            roomInfo.IsCondo = room.IsCondo;
            roomInfo.Smoking = room.Smoking;


            if (room.Beds == 1 && room.DoubleBeds == 0)
            {
                roomInfo.RoomType = RoomType.SingleBed;
            }
            else if (room.Beds == 1 && room.DoubleBeds == 1 )
            {
                roomInfo.RoomType = RoomType.DoubleBeds;
            }
            else if (room.Beds == 2 && room.DoubleBeds == 2)
            {
                roomInfo.RoomType = RoomType.MasterSuite;
            }
            else if (room.Beds == 2 && room.DoubleBeds == 1)
            {
                roomInfo.RoomType = RoomType.TripleBeds;
            }
            else if(room.Beds == 2 && room.DoubleBeds == 0)
            {
                roomInfo.RoomType = RoomType.TwinBeds;
            }

            return roomInfo;
        }
    }
}