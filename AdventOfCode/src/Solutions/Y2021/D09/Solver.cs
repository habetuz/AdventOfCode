using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2021.D09;

public class Solver : CustomGridSplittingSolver<byte>
{
  public override byte Convert(char value, int x, int y)
  {
    return (byte)(value - '0');
  }

  public override void Solve(byte[,] input, IPartSubmitter partSubmitter)
  {
    List<Coordinate> lowPoints = [];

    int riskLevel = 0;

    for (int y = 0; y < input.GetLength(1); y++)
    {
      for (int x = 0; x < input.GetLength(0); x++)
      {
        if (
          (x - 1 < 0 || input[x - 1, y] > input[x, y])
          && (x + 1 >= input.GetLength(0) || input[x + 1, y] > input[x, y])
          && (y - 1 < 0 || input[x, y - 1] > input[x, y])
          && (y + 1 >= input.GetLength(1) || input[x, y + 1] > input[x, y])
        )
        {
          riskLevel += input[x, y] + 1;
          lowPoints.Add((x, y));
          x++;
        }
      }
    }

    partSubmitter.SubmitPart1(riskLevel);

    List<int> basinSizes = [];

    foreach (var lowPoint in lowPoints)
    {
      basinSizes.Add(GetBasinSize(lowPoint, input));
    }

    basinSizes.Sort();
    basinSizes.Reverse();
    int solution = basinSizes[0] * basinSizes[1] * basinSizes[2];

    partSubmitter.SubmitPart2(solution);
  }

  private int GetBasinSize(Coordinate lowPoint, byte[,] map)
  {
    return GetBasingSize(lowPoint, map, new bool[map.GetLength(0), map.GetLength(1)]);
  }

  private int GetBasingSize(Coordinate point, byte[,] map, bool[,] visited)
  {
    visited[point.X, point.Y] = true;
    int size = 1;
    if (point.X - 1 >= 0 && !visited[point.X - 1, point.Y] && map[point.X - 1, point.Y] < 9)
    {
      size += GetBasingSize((point.X - 1, point.Y), map, visited);
    }

    if (
      point.X + 1 < map.GetLength(0)
      && !visited[point.X + 1, point.Y]
      && map[point.X + 1, point.Y] < 9
    )
    {
      size += GetBasingSize((point.X + 1, point.Y), map, visited);
    }

    if (point.Y - 1 >= 0 && !visited[point.X, point.Y - 1] && map[point.X, point.Y - 1] < 9)
    {
      size += GetBasingSize((point.X, point.Y - 1), map, visited);
    }

    if (
      point.Y + 1 < map.GetLength(1)
      && !visited[point.X, point.Y + 1]
      && map[point.X, point.Y + 1] < 9
    )
    {
      size += GetBasingSize((point.X, point.Y + 1), map, visited);
    }

    return size;
  }
}
