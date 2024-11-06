using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Solver.Templates;
using AdventOfCode.Utils;
using SharpLog;
using YamlDotNet.Core.Tokens;

namespace AdventOfCode.Solutions.Y2021.D05;

public class Solver : ISolver<(Line[], Coordinate)>
{
  public void Parse(string input, IPartSubmitter<(Line[], Coordinate)> partSubmitter)
  {
    int maxX = 0;
    int maxY = 0;
    Line[] lines = input
      .Split('\n')
      .Select(
        (
          line =>
          {
            string[] pointStrings = line.Split([" -> "], StringSplitOptions.RemoveEmptyEntries);
            Coordinate[] points = new Coordinate[2];
            for (int i = 0; i < pointStrings.Length; i++)
            {
              string[] coordinates = pointStrings[i].Split(',');
              int x = int.Parse(coordinates[0]);
              int y = int.Parse(coordinates[1]);
              points[i] = (x, y);

              if (x > maxX)
              {
                maxX = x;
              }

              if (y > maxY)
              {
                maxY = y;
              }
            }

            return new Line(points[0], points[1]);
          }
        )
      )
      .ToArray();

    partSubmitter.Submit((lines, (maxX + 1, maxY + 1)));
  }

  public void Solve((Line[], Coordinate) input, IPartSubmitter partSubmitter)
  {
    int[,] field1 = new int[input.Item2.X, input.Item2.Y];
    int[,] field2 = new int[input.Item2.X, input.Item2.Y];

    foreach (Line line in input.Item1)
    {
      foreach (Coordinate point in line.Points())
      {
        if (!line.IsDiagonal)
        {
          field1[point.X, point.Y]++;
        }

        field2[point.X, point.Y]++;
      }
    }

    int overlapCounter1 = 0;
    foreach (int i in field1)
    {
      if (i >= 2)
      {
        overlapCounter1++;
      }
    }

    partSubmitter.SubmitPart1(overlapCounter1);

    int overlapCounter2 = 0;
    foreach (int i in field2)
    {
      if (i >= 2)
      {
        overlapCounter2++;
      }
    }

    partSubmitter.SubmitPart2(overlapCounter2);
  }
}
