
public enum TileType { empty, wood, stone, grass, water, lava }
public enum Direction
{
    none = 0,
    North = 1,
    East = 2,
    South = 4,
    West = 8,

    All = North + East + South + West,

    NorthEast = 3,
    NorthEastSouth = 7,
    NorthSouth = 5,
    NorthSouthWest = 13,
    NorthWest = 9,
    EastSouth = 6,
    EastSouthWest = 14,
    EastWest = 10,
    SouthWest = 12,


    corner_NE = 16,
    corner_SE = 32,
    corner_SW = 64,
    corner_NW = 128
}

public class oTile
{
    public oRoom Parent;
    public int ID;
    public int X { get { return ID % 5; } }
    public int Y { get { return (ID - X) / 5; } }
    public int RoomX;
    public int RoomY;
    public int GlobalX { get { return X + (RoomX * 5); } }
    public int GlobalY { get { return Y + (RoomY * 5); } }

    public TileType Type;
    public Direction Walls;
    public Direction Exit;

    public bool isHover;
    public bool isSelectable;
    public bool isSelected; //??
    public bool isBlocked;
    public string Token;


    public oTile(int ID, int RoomX, int RoomY)
    {
        this.ID = ID;
        this.RoomX = RoomX;
        this.RoomY = RoomY;

        isHover = false;
        isSelected = false;
        isSelectable = false;
        isBlocked = false;
        Token = "";
    }

    public oTile(int ID, int RoomX, int RoomY, TileType Type)
        : this(ID, RoomX, RoomY)
    {
        Direction walls = Direction.none;
        if (X == 0) { Walls |= Direction.West; }
        if (X == 4) { Walls |= Direction.East; }
        if (Y == 0) { Walls |= Direction.North; }
        if (Y == 4) { Walls |= Direction.South; }

        this.Type = Type;
        this.Walls = walls;
    }

    public oTile(int ID, int RoomX, int RoomY, TileType Type, Direction Walls)
        : this(ID, RoomX, RoomY)
    {
        this.Type = Type;
        this.Walls = Walls;

        if (X == 0 && Y == 2 && (Walls & Direction.West) == Direction.none)
        {
            Exit |= Direction.West;
        }
        if (X == 4 && Y == 2 && (Walls & Direction.East) == Direction.none)
        {
            Exit |= Direction.East;
        }
        if (Y == 0 && X == 2 && (Walls & Direction.North) == Direction.none)
        {
            Exit |= Direction.North;
        }
        if (Y == 4 && X == 2 && (Walls & Direction.South) == Direction.none)
        {
            Exit |= Direction.South;
        }

    }
}
