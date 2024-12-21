using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2024.D04;

public class Solver : GridSplittingSolver
{
  public override void Solve(char[,] input, IPartSubmitter partSubmitter)
  {
    int xmasCount1 = 0;
    int xmasCount2 = 0;
    Coordinate downRight = (input.GetLength(0) - 1, input.GetLength(1) - 1);

    foreach ((var value, var coordinate) in Array2D.Enumerate(input))
    {
      if (value == 'X')
      {
        foreach (Direction direction in Direction.All.Iterate())
        {
          if (
            new string((char[])[value, .. Array2D.GetInDirection(input, coordinate, direction, 3)])
            == "XMAS"
          )
          {
            xmasCount1++;
          }
        }
      }
      else if (
        value == 'A'
        && (Direction.UpLeft | Direction.UpRight | Direction.DownLeft | Direction.DownRight)
          .Iterate()
          .All((direction) => (coordinate + direction).IsInSpace(downRight))
        && (
          (
            Array2D.GetInDirection(input, coordinate, Direction.UpLeft) == 'M'
            && Array2D.GetInDirection(input, coordinate, Direction.DownRight) == 'S'
          )
          || (
            Array2D.GetInDirection(input, coordinate, Direction.UpLeft) == 'S'
            && Array2D.GetInDirection(input, coordinate, Direction.DownRight) == 'M'
          )
        )
        && (
          (
            Array2D.GetInDirection(input, coordinate, Direction.UpRight) == 'M'
            && Array2D.GetInDirection(input, coordinate, Direction.DownLeft) == 'S'
          )
          || (
            Array2D.GetInDirection(input, coordinate, Direction.UpRight) == 'S'
            && Array2D.GetInDirection(input, coordinate, Direction.DownLeft) == 'M'
          )
        )
      ) { 
        xmasCount2++;
      }
    }

    partSubmitter.SubmitPart1(xmasCount1);
    partSubmitter.SubmitPart2(xmasCount2);
  }
}
