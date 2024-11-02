using System.ComponentModel.DataAnnotations;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using SharpLog;

namespace AdventOfCode.Solutions.Y2023.D23;

public class Solver : ISolver<Graph, Graph>
{
  public void Parse(string input, IPartSubmitter<Graph, Graph> partSubmitter)
  {
    char[,] map = Array2D.FromString(input);
    Node?[,] nodes1 = new Node?[map.GetLength(0), map.GetLength(1)];
    Node?[,] nodes2 = new Node?[map.GetLength(0), map.GetLength(1)];
    Graph graph1 = new();
    Graph graph2 = new();
    graph1.Map = map;
    graph2.Map = map;

    // Initialize nodes
    for (int y = 0; y < map.GetLength(1); y++)
    {
      for (int x = 0; x < map.GetLength(0); x++)
      {
        if (map[x, y] == '.')
        {
          var node1 = new Node { Position = new Coordinate(x, y) };
          var node2 = new Node { Position = new Coordinate(x, y) };
          nodes1[x, y] = node1;
          nodes2[x, y] = node2;
          graph1.AddNode(node1);
          graph2.AddNode(node2);
        }
        else if (map[x, y] != '#')
        {
          var node2 = new Node { Position = new Coordinate(x, y) };
          nodes2[x, y] = node2;
        }
      }
    }

    graph1.Start = nodes1[1, 0]!;
    graph2.Start = nodes2[1, 0]!;
    graph1.End = nodes1[nodes1.GetLength(0) - 2, nodes1.GetLength(1) - 1]!;
    graph2.End = nodes2[nodes1.GetLength(0) - 2, nodes2.GetLength(1) - 1]!;

    // Add edges
    for (int y = 0; y < map.GetLength(1); y++)
    {
      for (int x = 0; x < map.GetLength(0); x++)
      {
        if (map[x, y] == '#')
        {
          continue;
        }

        if (x > 0)
        {
          if (map[x - 1, y] != '#')
          {
            graph2.AddEdge(nodes2[x, y]!, nodes2[x - 1, y]!, 1);
          }

          if (map[x, y] == '.')
          {
            if (map[x - 1, y] == '.')
            {
              graph1.AddEdge(nodes1[x, y]!, nodes1[x - 1, y]!, 1);
            }
            else if (map[x - 1, y] == '<')
            {
              graph1.AddEdge(nodes1[x, y]!, nodes1[x - 2, y]!, 2);
            }
          }
        }

        if (x < map.GetLength(0) - 1)
        {
          if (map[x + 1, y] != '#')
          {
            graph2.AddEdge(nodes2[x, y]!, nodes2[x + 1, y]!, 1);
          }

          if (map[x, y] == '.')
          {
            if (map[x + 1, y] == '.')
            {
              graph1.AddEdge(nodes1[x, y]!, nodes1[x + 1, y]!, 1);
            }
            else if (map[x + 1, y] == '>')
            {
              graph1.AddEdge(nodes1[x, y]!, nodes1[x + 2, y]!, 2);
            }
          }
        }

        if (y > 0)
        {
          if (map[x, y - 1] != '#')
          {
            graph2.AddEdge(nodes2[x, y]!, nodes2[x, y - 1]!, 1);
          }

          if (map[x, y] == '.')
          {
            if (map[x, y - 1] == '.')
            {
              graph1.AddEdge(nodes1[x, y]!, nodes1[x, y - 1]!, 1);
            }
            else if (map[x, y - 1] == '^')
            {
              graph1.AddEdge(nodes1[x, y]!, nodes1[x, y - 2]!, 2);
            }
          }
        }

        if (y < map.GetLength(1) - 1)
        {
          if (map[x, y + 1] != '#')
          {
            graph2.AddEdge(nodes2[x, y]!, nodes2[x, y + 1]!, 1);
          }

          if (map[x, y] == '.')
          {
            if (map[x, y + 1] == '.')
            {
              graph1.AddEdge(nodes1[x, y]!, nodes1[x, y + 1]!, 1);
            }
            else if (map[x, y + 1] == 'v')
            {
              graph1.AddEdge(nodes1[x, y]!, nodes1[x, y + 2]!, 2);
            }
          }
        }
      }
    }

    // Remove nodes with only one or two neighbors
    var intersections2 = graph2
      .Nodes.Where((node) => node.Neighbors.Count > 2)
      .Append(graph2.Start)
      .Append(graph2.End);

    foreach (var node in intersections2)
    {
      var neighbors = node.Neighbors;
      HashSet<(int distance, Node node)> newNeighbors = [];
      foreach (var (distance, neighbor) in neighbors)
      {
        var prePosition = node;
        var position = neighbor;
        var distanceSum = distance;

        do
        {
          var next = position.Neighbors.First((neighbor) => neighbor.node != prePosition);
          distanceSum += next.distance;
          prePosition = position;
          position = next.node;
        } while (!intersections2.Contains(position));

        newNeighbors.Add((distanceSum, position));
      }

      node.Neighbors = newNeighbors;
    }

    partSubmitter.SubmitPart1(graph1);
    partSubmitter.SubmitPart2(graph2);
  }

  public void Solve(Graph input1, Graph input2, IPartSubmitter partSubmitter)
  {
    partSubmitter.SubmitPart1(new Hiker([], input1.Start).Walk(input1.End, input1.Map));
    partSubmitter.SubmitPart2(new Hiker([], input2.Start).Walk(input2.End, input2.Map));

    // Queue<Node> queue = new();
    // input2.End.DistanceFromGoal = 0;
    // queue.Enqueue(input2.End);
    // while (queue.TryDequeue(out var node))
    // {
    //     Array2D.Print(
    //         (uint)input2.Map.GetLength(0),
    //         (uint)input2.Map.GetLength(1),
    //         (x, y) =>
    //             input2.Map[x, y] switch
    //             {
    //                 'O' => "[yellow]O[/]",
    //                 '#' => "[gray]#[/]",
    //                 _ => "[gray].[/]"
    //             }
    //     );

    //     input2.Map[node.Position.X, node.Position.Y] = 'O';
    //     foreach (var (_, neighbor) in node.Neighbors)
    //     {
    //         if (node.Parent == neighbor)
    //         {
    //             continue;
    //         }

    //         if (neighbor.Parent == null)
    //         {
    //             neighbor.DistanceFromGoal = node.DistanceFromGoal + 1;
    //             queue.Enqueue(neighbor);
    //             continue;
    //         }

    //         if (neighbor.DistanceFromGoal < node.DistanceFromGoal + 1)
    //         {
    //             neighbor.DistanceFromGoal = node.DistanceFromGoal + 1;
    //             queue.Enqueue(neighbor);
    //         }
    //     }
    // }

    // partSubmitter.SubmitPart2(input2.Start.DistanceFromGoal);
  }
}
