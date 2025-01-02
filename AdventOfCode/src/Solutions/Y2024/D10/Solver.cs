using System.Globalization;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using MathNet.Numerics.LinearAlgebra.Complex.Solvers;
using Spectre.Console;
using YamlDotNet.Core.Events;
using static AdventOfCode.Solutions.Y2024.D10.Solver;

namespace AdventOfCode.Solutions.Y2024.D10;

public class Solver : ISolver<Node[]>
{
  public void Parse(string input, IPartSubmitter<Node[]> partSubmitter)
  {
    var map = Array2D.FromString(input);
    var nodesMap = new Node[map.GetLength(0), map.GetLength(1)];
    var nodesList = new List<Node>();

    var downRight = (map.GetLength(0) - 1, map.GetLength(1) - 1);
    foreach ((char value, Coordinate coordinate) in Array2D.Enumerate(map))
    {
      Array2D.IterateAroundCoordinate(
        map,
        coordinate,
        (map, neighbor, direction) =>
        {
          if (!neighbor.IsInSpace(downRight) || map[neighbor.X, neighbor.Y] != value + 1)
          {
            return Direction.None;
          }

          nodesMap[coordinate.X, coordinate.Y] ??= new((byte)(value - '0'));
          nodesMap[neighbor.X, neighbor.Y] ??= new((byte)(map[neighbor.X, neighbor.Y] - '0'));
          nodesMap[coordinate.X, coordinate.Y].Neighbors.Add(nodesMap[neighbor.X, neighbor.Y]);
          return Direction.None;
        },
        Direction.UpLeft | Direction.UpRight | Direction.DownLeft | Direction.DownRight
      );

      if (value == '0')
      {
        nodesMap[coordinate.X, coordinate.Y] ??= new(0);
      }
    }

    foreach (var node in nodesMap)
    {
      if (node is not null)
        nodesList.Add(node);
    }

    partSubmitter.Submit([.. nodesList]);
  }

  public void Solve(Node[] input, IPartSubmitter partSubmitter)
  {
    var trailHeads = input.Where((node) => node.Height == 0);

    (var distinctEnds, var variants) = trailHeads
      .AsParallel()
      .Select(Walk)
      .Select(nodes => (nodes.Distinct().Count(), nodes.Count()))
      .Aggregate((acc, curr) => (acc.Item1 + curr.Item1, acc.Item2 + curr.Item2));

    partSubmitter.SubmitPart1(distinctEnds);
    partSubmitter.SubmitPart2(variants);
  }

  public IEnumerable<Node> Walk(Node node)
  {
    if (node.Height == 9)
    {
      return [node];
    }

    if (node.Neighbors.Count == 0)
    {
      return [];
    }

    return node.Neighbors.SelectMany(Walk);
  }

  public class Node(byte height)
  {
    public List<Node> Neighbors { get; set; } = [];

    public uint Height { get; set; } = height;
  }
}
