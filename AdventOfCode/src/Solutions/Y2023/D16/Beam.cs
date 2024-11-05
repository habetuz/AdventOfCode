using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D16;

public class Beam
{
  public Coordinate Coordinate { get; set; }

  public Direction Direction { get; set; }

  public void Shoot(char[,] input, Direction[,] discovered)
  {
    while (true)
    {
      Coordinate += Direction;
      if (
        Coordinate.X < 0
        || Coordinate.X >= input.GetLength(0)
        || Coordinate.Y < 0
        || Coordinate.Y >= input.GetLength(1)
      )
      {
        return;
      }

      if (discovered[Coordinate.X, Coordinate.Y].HasFlag(Direction))
      {
        return;
      }

      discovered[Coordinate.X, Coordinate.Y] |= Direction;

      switch (input[Coordinate.X, Coordinate.Y])
      {
        case '/':
          if (Direction == Direction.Right || Direction == Direction.Left)
          {
            Direction = Direction.Rotate90Left();
          }
          else
          {
            Direction = Direction.Rotate90Right();
          }
          break;
        case '\\':
          if (Direction == Direction.Right || Direction == Direction.Left)
          {
            Direction = Direction.Rotate90Right();
          }
          else
          {
            Direction = Direction.Rotate90Left();
          }
          break;
        case '-':
          if (Direction == Direction.Left || Direction == Direction.Right)
          {
            break;
          }
          new Beam { Coordinate = Coordinate, Direction = Direction.Rotate90Left() }.Shoot(
            input,
            discovered
          );
          Direction = Direction.Rotate90Right();
          break;
        case '|':
          if (Direction == Direction.Up || Direction == Direction.Down)
          {
            break;
          }
          new Beam { Coordinate = Coordinate, Direction = Direction.Rotate90Left() }.Shoot(
            input,
            discovered
          );
          Direction = Direction.Rotate90Right();
          break;
      }
    }
  }
}
