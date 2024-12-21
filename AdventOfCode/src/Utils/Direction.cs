namespace AdventOfCode.Utils;

[Flags]
public enum Direction
{
  None = 0,
  UpLeft = 1,
  Up = 2,
  UpRight = 4,
  Right = 8,
  DownRight = 16,
  Down = 32,
  DownLeft = 64,
  Left = 128,
  All = UpLeft | Up | UpRight | Left | Right | DownLeft | Down | DownRight,
}
