using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D16;

public class Solver : ISolver<char[,]>
{
  public void Parse(string input, IPartSubmitter<char[,]> partSubmitter)
  {
    partSubmitter.Submit(Array2D.FromString(input));
  }

  public void Solve(char[,] input, IPartSubmitter partSubmitter)
  {
    Direction[,] discovered = new Direction[input.GetLength(0), input.GetLength(1)];

    new Beam { Coordinate = new Coordinate(-1, 0), Direction = Direction.Right }.Shoot(
      input,
      discovered
    );

    int energized = discovered.Cast<Direction>().Count(d => d != Direction.None);

    partSubmitter.SubmitPart1(energized);

    int max = 0;

    for (int x = 0; x < input.GetLength(0); x++)
    {
      discovered = new Direction[input.GetLength(0), input.GetLength(1)];
      new Beam() { Coordinate = new Coordinate(x, 0 - 1), Direction = Direction.Down }.Shoot(
        input,
        discovered
      );
      energized = discovered.Cast<Direction>().Count(d => d != Direction.None);

      if (energized > max)
      {
        max = energized;
      }

      discovered = new Direction[input.GetLength(0), input.GetLength(1)];
      new Beam()
      {
        Coordinate = new Coordinate(x, input.GetLength(1) - 1 + 1),
        Direction = Direction.Up,
      }.Shoot(input, discovered);
      energized = discovered.Cast<Direction>().Count(d => d != Direction.None);

      if (energized > max)
      {
        max = energized;
      }
    }

    for (int y = 0; y < input.GetLength(1); y++)
    {
      discovered = new Direction[input.GetLength(0), input.GetLength(1)];
      new Beam() { Coordinate = new Coordinate(0 - 1, y), Direction = Direction.Right }.Shoot(
        input,
        discovered
      );
      energized = discovered.Cast<Direction>().Count(d => d != Direction.None);

      if (energized > max)
      {
        max = energized;
      }

      discovered = new Direction[input.GetLength(0), input.GetLength(1)];
      new Beam()
      {
        Coordinate = new Coordinate(input.GetLength(0) - 1 + 1, y),
        Direction = Direction.Left,
      }.Shoot(input, discovered);
      energized = discovered.Cast<Direction>().Count(d => d != Direction.None);

      if (energized > max)
      {
        max = energized;
      }
    }

    partSubmitter.SubmitPart2(max);
  }
}
