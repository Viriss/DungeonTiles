using System.Collections.Generic;

public class oFloor
{
    public int Number;
    public List<oRoom> Rooms;

    public oFloor()
    {
        Number = -1;
        Rooms = new List<oRoom>();
    }

    public void AddRoom(int X, int Y, RoomPattern Pattern)
    {
        oRoom r = new oRoom();

        if (Pattern == null)
        {
            r.CreateEmptyRoom(TileType.wood, Direction.All);
        }
        else
        {
            r.CreateRoomFromPattern(Pattern);
        }
        Rooms.Add(r);
    }

}
