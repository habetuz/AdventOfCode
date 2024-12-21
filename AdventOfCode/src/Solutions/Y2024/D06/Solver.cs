using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using MathNet.Numerics.Distributions;

namespace AdventOfCode.Solutions.Y2024.D06;

public class Solver
  : ISolver<(
    HashSet<Coordinate> Obstacles,
    Coordinate Guard,
    Direction Direction,
    Coordinate DownRight
  )>
{
  public void Parse(
    string input,
    IPartSubmitter<(
      HashSet<Coordinate> Obstacles,
      Coordinate Guard,
      Direction Direction,
      Coordinate DownRight
    )> partSubmitter
  )
  {
    var lines = input.Split('\n');
    HashSet<Coordinate> obstacles = [];
    Coordinate? guard = null;
    for (int y = 0; y < lines.Length; y++)
    {
      for (int x = 0; x < lines[0].Length; x++)
      {
        if (lines[y][x] == '#')
        {
          obstacles.Add((x, y));
        }
        if (lines[y][x] == '^')
        {
          guard = (x, y);
        }
      }
    }

    if (!guard.HasValue)
    {
      throw new Exception("No guard was found in map!");
    }

    partSubmitter.Submit(
      (obstacles, guard.Value, Direction.Up, (lines[0].Length - 1, lines.Length - 1))
    );
  }

  public void Solve(
    (
      HashSet<Coordinate> Obstacles,
      Coordinate Guard,
      Direction Direction,
      Coordinate DownRight
    ) input,
    IPartSubmitter partSubmitter
  )
  {
    Coordinate guard = input.Guard;
    Direction direction = input.Direction;
    HashSet<Coordinate> visited = [];

    visited.Add(guard);

    while (true)
    {
      var step = guard + direction;

      if (!step.IsInSpace(input.DownRight))
      {
        break;
      }

      if (input.Obstacles.Contains(step))
      {
        direction = direction.Rotate90Right();
      }
      else
      {
        guard = step;
      }

      visited.Add(guard);
    }

    partSubmitter.SubmitPart1(visited.Count);

    int workingObstacles = 0;

    foreach (Coordinate additionalObstacle in visited)
    {
      guard = input.Guard;
      direction = input.Direction;
      input.Obstacles.Add(additionalObstacle);
      Dictionary<Coordinate, Direction> positions = [];

      positions[guard] = direction;

      while (true)
      {
        var step = guard + direction;

        if (!step.IsInSpace(input.DownRight))
        {
          break;
        }

        if (input.Obstacles.Contains(step))
        {
          direction = direction.Rotate90Right();
        }
        else
        {
          guard = step;
        }

        if (positions.GetValueOrDefault(guard, Direction.None).HasFlag(direction))
        {
          workingObstacles++;
          break;
        }

        positions[guard] = direction | positions.GetValueOrDefault(guard, Direction.None);
      }

      input.Obstacles.Remove(additionalObstacle);
    }

    partSubmitter.SubmitPart2(workingObstacles);
  }
}
