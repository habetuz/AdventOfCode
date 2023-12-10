namespace AdventOfCode.Utils;

[Flags]
public enum Direction
{
    None = 0,
    UpLeft = 1,
    Up = 2,
    UpRight = 4,
    Left = 8,
    Right = 16,
    DownLeft = 32,
    Down = 64,
    DownRight = 128,
    All = UpLeft | Up | UpRight | Left | Right | DownLeft | Down | DownRight,
}
