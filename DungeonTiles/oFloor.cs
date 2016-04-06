using System;
using System.Collections.Generic;

public class oFloor
{
    public int Number;
    public List<oRoom> Rooms;

    public delegate void DoDrawRooms();
    public event DoDrawRooms DrawRooms;

    public oFloor()
    {
        Number = -1;
        Rooms = new List<oRoom>();
    }

    public void AddRoom(int X, int Y, RoomPattern Pattern)
    {
        oRoom r = new oRoom(Pattern.Name, X, Y);

        if (Pattern == null)
        {
            r.CreateEmptyRoom(TileType.wood, Direction.All);
        }
        else
        {
            r.CreateRoomFromPattern(Pattern);
        }
        r.Parent = this;
        Rooms.Add(r);
    }

    public void ClearHover()
    {
        foreach (oRoom r in Rooms)
        {
            r.ClearHover();
        }
    }
    public void ClearSelectable()
    {
        foreach (oRoom r in Rooms)
        {
            r.ClearSelectable();
        }
    }

    public void DoRoomUpdate()
    {
        DrawRooms();
    }

    public List<oTile> FindAdjacentTiles(oTile tile)
    {
        List<oTile> list = new List<oTile>();
        oTile t;

        t = FindTileInDirection(tile, Direction.North);
        if (t != null) { list.Add(t); }
        t = FindTileInDirection(tile, Direction.South);
        if (t != null) { list.Add(t); }
        t = FindTileInDirection(tile, Direction.East);
        if (t != null) { list.Add(t); }
        t = FindTileInDirection(tile, Direction.West);
        if (t != null) { list.Add(t); }

        t = FindTileInDirection(tile, Direction.NorthEast);
        if (t != null) { list.Add(t); }
        t = FindTileInDirection(tile, Direction.EastSouth);
        if (t != null) { list.Add(t); }
        t = FindTileInDirection(tile, Direction.NorthWest);
        if (t != null) { list.Add(t); }
        t = FindTileInDirection(tile, Direction.SouthWest);
        if (t != null) { list.Add(t); }

        return list;
    }
    public oTile FindTileInDirection(oTile source, Direction direction)
    {
        int RoomX = source.Parent.RoomX;
        int RoomY = source.Parent.RoomY;
        int X = source.X;
        int Y = source.Y;

        if ((direction & Direction.North) == Direction.North &&
            (source.Walls & Direction.North) == Direction.none)
        {
            Y--;
            if (Y < 0)
            {
                RoomY--;
                Y += 5;
            }
        }
        if ((direction & Direction.South) == Direction.South &&
            (source.Walls & Direction.South) == Direction.none)
        {
            Y++;
            if (Y > 4)
            {
                RoomY++;
                Y -= 5;
            }
        }
        if ((direction & Direction.East) == Direction.East &&
            (source.Walls & Direction.East) == Direction.none)
        {
            X++;
            if (X > 4)
            {
                RoomX++;
                X -= 5;
            }
        }
        if ((direction & Direction.West) == Direction.West &&
            (source.Walls & Direction.West) == Direction.none)
        {
            X--;
            if (X < 0)
            {
                RoomX--;
                X += 5;
            }
        }

        oRoom r = GetRoomByCoor(RoomX, RoomY);
        oTile t;

        if (r != null)
        {
            t = r.GetTileByCoor(X, Y);
            if (t != null)
            {
                return t;
            }
        }

        return null;
    }

    public oRoom GetRoomByID(Guid RoomID)
    {
        foreach (oRoom r in Rooms)
        {
            if (r.ID == RoomID) { return r; }
        }
        return null;
    }
    public oRoom GetRoomByCoor(int X, int Y)
    {
        foreach (oRoom r in Rooms)
        {
            if (r.RoomX == X && r.RoomY == Y) { return r; }
        }
        return null;
    }

    public void SelectTile(Guid RoomID, int TileID)
    {
        ClearSelectable();
        oRoom r = GetRoomByID(RoomID);
        r.GetTileByIndex(TileID).isSelectable = true;

        foreach (oTile t in FindAdjacentTiles(r.GetTileByIndex(TileID)))
        {
            t.isSelectable = true;
        }

        DoRoomUpdate();
    }
}
