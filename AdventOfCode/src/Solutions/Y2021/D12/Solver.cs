using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2021.D12;

public class Solver : ISolver<Node>
{
  public void Parse(string input, IPartSubmitter<Node> partSubmitter)
  {
    string[] lines = input.Split('\n');

    Dictionary<string, Node> nodes = [];

    foreach (string line in lines)
    {
      string[] connections = line.Split('-');

      if (!nodes.TryGetValue(connections[0], out Node? nodeA))
      {
        nodeA = new Node(connections[0]);
        nodes.Add(connections[0], nodeA);
      }

      if (!nodes.TryGetValue(connections[1], out Node? nodeB))
      {
        nodeB = new Node(connections[1]);
        nodes.Add(connections[1], nodeB);
      }

      var nodeAConnections = nodeA.Connections;
      Array.Resize(ref nodeAConnections, nodeA.Connections.Length + 1);
      nodeA.Connections = nodeAConnections;

      nodeA.Connections[^1] = nodeB;

      if (connections[0] != "start" && connections[1] != "end")
      {
        var nodeBConnections = nodeB.Connections;
        Array.Resize(ref nodeBConnections, nodeB.Connections.Length + 1);
        nodeB.Connections = nodeBConnections;
        nodeB.Connections[nodeB.Connections.Length - 1] = nodeA;
      }
    }

    partSubmitter.Submit(nodes["start"]);
  }

  public void Solve(Node input, IPartSubmitter partSubmitter)
  {
    partSubmitter.SubmitPart1(FindAllPaths(input, true));
    partSubmitter.SubmitPart2(FindAllPaths(input, false));
  }

  private static int FindAllPaths(Node input, bool visitedTwice)
  {
    return FindAllPaths(input, [], visitedTwice);
  }

  private static int FindAllPaths(Node input, List<Node> visitedSmallNodes, bool visitedTwice)
  {
    if (input.Name == "end")
    {
      return 1;
    }

    visitedSmallNodes = visitedSmallNodes.ToList();
    if (!input.IsBig)
    {
      visitedSmallNodes.Add(input);
    }

    int paths = 0;

    foreach (Node node in input.Connections)
    {
      if (node.IsBig || !visitedSmallNodes.Contains(node) || !visitedTwice)
      {
        paths += FindAllPaths(
          node,
          visitedSmallNodes,
          visitedSmallNodes.Contains(node) || visitedTwice
        );
      }
    }

    return paths;
  }
}
