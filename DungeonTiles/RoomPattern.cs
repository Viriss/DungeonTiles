using System.Collections.Generic;

public class RoomPattern
{
    public string Name;
    public Direction Exits;
    public string Pattern;

}

public class PatternBook
{
    public List<RoomPattern> Patterns;

    public PatternBook()
    {
        Patterns = new List<RoomPattern>();
    }
}
