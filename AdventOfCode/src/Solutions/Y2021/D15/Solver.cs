using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;

namespace AdventOfCode.Solutions.Y2021.D15;

public class Solver : CustomGridSplittingSolver<Node>
{
  public override Node Convert(char value, int x, int y)
  {
    return new Node(value - '0', x, y);
  }

  public override void Solve(Node[,] input, IPartSubmitter partSubmitter)
  {
    List<Node> queue = [input[0, 0]];

    int riskLevel;

    int iterationCount = 0;

    while (true)
    {
      iterationCount++;

      Node node = queue[0];

      if (node.X == input.GetLength(0) - 1 && node.Y == input.GetLength(1) - 1)
      {
        riskLevel = node.F;
        break;
      }

      queue.RemoveAt(0);
      node.Discovered = true;

      if (node.X - 1 >= 0)
      {
        DiscoverNode(node, input[node.X - 1, node.Y], queue);
      }

      if (node.Y - 1 >= 0)
      {
        DiscoverNode(node, input[node.X, node.Y - 1], queue);
      }

      if (node.X + 1 < input.GetLength(0))
      {
        DiscoverNode(node, input[node.X + 1, node.Y], queue);
      }

      if (node.Y + 1 < input.GetLength(1))
      {
        DiscoverNode(node, input[node.X, node.Y + 1], queue);
      }
    }

    partSubmitter.SubmitPart1(riskLevel);

    Node[,] map = new Node[input.GetLength(0) * 5, input.GetLength(1) * 5];

    for (int tileX = 0; tileX < 5; tileX++)
    {
      for (int tileY = 0; tileY < 5; tileY++)
      {
        for (int x = 0; x < input.GetLength(0); x++)
        {
          for (int y = 0; y < input.GetLength(1); y++)
          {
            riskLevel = input[x, y].RiskLevel;
            riskLevel += tileX + tileY;
            if (riskLevel > 9)
            {
              riskLevel -= 9;
            }

            map[(tileX * 100) + x, (tileY * 100) + y] = new Node(
              riskLevel,
              (tileX * 100) + x,
              (tileY * 100) + y
            );
          }
        }
      }
    }

    queue = [map[0, 0]];

    int pathRiskLevel;

    while (true)
    {
      Node node = queue[0];

      if (node.X == map.GetLength(0) - 1 && node.Y == map.GetLength(1) - 1)
      {
        pathRiskLevel = node.F;
        break;
      }

      queue.RemoveAt(0);
      node.Discovered = true;

      if (node.X - 1 >= 0)
      {
        DiscoverNode(node, map[node.X - 1, node.Y], queue);
      }

      if (node.Y - 1 >= 0)
      {
        DiscoverNode(node, map[node.X, node.Y - 1], queue);
      }

      if (node.X + 1 < map.GetLength(0))
      {
        DiscoverNode(node, map[node.X + 1, node.Y], queue);
      }

      if (node.Y + 1 < map.GetLength(1))
      {
        DiscoverNode(node, map[node.X, node.Y + 1], queue);
      }
    }

    partSubmitter.SubmitPart2(pathRiskLevel);
  }

  private static void DiscoverNode(Node origin, Node node, List<Node> queue)
  {
    if (node.Discovered)
    {
      return;
    }

    int f = origin.F + node.RiskLevel;

    node.F = node.F == 0 || node.F > f ? f : node.F;

    while (queue.Remove(node)) { }

    AddToQueue(node, queue);
  }

  private static void AddToQueue(Node node, List<Node> queue)
  {
    for (int i = queue.Count - 1; i >= 0; i--)
    {
      if (node.CompareTo(queue[i]) >= 0)
      {
        if (i + 1 < queue.Count)
        {
          queue.Insert(i + 1, node);
          return;
        }

        break;
      }
    }

    queue.Add(node);
  }
}
