using System;
using System.Collections.Generic;

public class oRoom
{
    public string Name;
    public int RoomX;
    public int RoomY;
    public List<oTile> Tiles;

    public oRoom()
    {
        Tiles = new List<oTile>();
    }

    public void AddTile(int X, int Y, TileType Type, Direction Walls)
    {
        int index = CoorToIndex(X, Y);
        oTile t = new oTile(index, RoomX, RoomY);
        t.Type = Type;
        t.Walls = Walls;
        Tiles.Add(t);
    }
    public void CreateEmptyRoom(TileType Type, Direction Exits)
    {
        Tiles = new List<oTile>();
        Direction Walls = Direction.All;
        Walls ^= Exits;

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                AddTile(x, y, Type, Walls);
            }
        }
    }
    public void CreateRoomFromPattern(RoomPattern Pattern)
    {
        this.Name = Pattern.Name;
        string[] rows = Pattern.Pattern.Split(new string[] { Environment.NewLine, "\n", "|" }, StringSplitOptions.None);
            
        CreateTilesFromPattern(rows);
        CreateWallsFromEdges(Pattern.Exits);
    }

    public int CoorToIndex(int X, int Y)
    {
        return X + (Y * 5);
    }
    public oTile GetTileByCoor(int X, int Y)
    {
        foreach (oTile t in Tiles)
        {
            if (t.X == X && t.Y == Y) { return t; }
        }
        return null;
    }
    public oTile GetTileByIndex(int Index)
    {
        foreach (oTile t in Tiles)
        {
            if (t.ID == Index) { return t; }
        }
        return null;
    }
    //public Point IndexToCoor(int Index)
    //{
    //    int x;
    //    int y;

    //    x = Index % 5;
    //    y = Index / 5;

    //    return new Point(x, y);
    //}
                
    private void CreateTilesFromPattern(string[] rows)
    {
        int rowIndex = 0;
        int index = 0;
        TileType tileType = TileType.empty;

        foreach (string row in rows)
        {
            index = 0;

            foreach (char t in row)
            {
                tileType = TileType.empty;
                switch (t)
                {
                    case '-':
                        tileType = TileType.wood;
                        break;
                    case ',':
                        tileType = TileType.grass;
                        break;
                    case '_':
                        tileType = TileType.stone;
                        break;
                    case 'w':
                        tileType = TileType.water;
                        break;
                    case 'l':
                        tileType = TileType.lava;
                        break;
                }
                if (tileType != TileType.empty)
                {
                    AddTile(index, rowIndex, tileType, Direction.none);
                }

                index++;
                if (index >= 5) { break; }
            }

            rowIndex++;
            if (rowIndex >= 5) { break; }
        }
    }
    private void CreateWallsFromEdges(Direction Exits)
    {
        oTile t;

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                t = GetTileByCoor(x, y);

                if (t != null)
                {
                    t.Walls = LookAtNeighbors(x, y);

                    if ((t.Walls & Exits) == Direction.North && x == 2 && y == 0) { t.Walls ^= Direction.North | Direction.corner_NE | Direction.corner_NW; }
                    if ((t.Walls & Exits) == Direction.East && x == 4 && y == 2) { t.Walls ^= Direction.East | Direction.corner_NE | Direction.corner_SE; }
                    if ((t.Walls & Exits) == Direction.South && x == 2 && y == 4) { t.Walls ^= Direction.South | Direction.corner_SE | Direction.corner_SW; }
                    if ((t.Walls & Exits) == Direction.West && x == 0 && y == 2) { t.Walls ^= Direction.West | Direction.corner_NW | Direction.corner_SW; }
                }
            }
        }
    }
    private Direction LookAtNeighbors(int x, int y)
    {
        oTile t_north;
        oTile t_south;
        oTile t_east;
        oTile t_west;
        oTile t_NE;
        oTile t_SE;
        oTile t_SW;
        oTile t_NW;
        Direction walls = Direction.none;

        t_north = GetTileByCoor(x, y - 1);
        t_south = GetTileByCoor(x, y + 1);
        t_west = GetTileByCoor(x - 1, y);
        t_east = GetTileByCoor(x + 1, y);
        t_NE = GetTileByCoor(x + 1, y - 1);
        t_SE = GetTileByCoor(x + 1, y + 1);
        t_SW = GetTileByCoor(x - 1, y + 1);
        t_NW = GetTileByCoor(x - 1, y - 1);

        if (t_north == null) { walls |= Direction.North; }
        if (t_south == null) { walls |= Direction.South; }
        if (t_west == null) { walls |= Direction.West; }
        if (t_east == null) { walls |= Direction.East; }
        if (t_north != null && t_east != null && t_NE == null) { walls |= Direction.corner_NE; }
        if (t_south != null && t_east != null && t_SE == null) { walls |= Direction.corner_SE; }
        if (t_south != null && t_west != null && t_SW == null) { walls |= Direction.corner_SW; }
        if (t_north != null && t_west != null && t_NW == null) { walls |= Direction.corner_NW; }

        return walls;
    }
}
