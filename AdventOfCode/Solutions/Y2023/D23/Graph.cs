namespace AdventOfCode.Solutions.Y2023.D23;

public class Graph
{
    public HashSet<Node> Nodes { get; set; } = new();

    public Node Start { get; set; } = null!;
    public Node End { get; set; } = null!;

    public char[,] Map {get; set;} = null!;

    public void AddNode(Node node)
    {
        Nodes.Add(node);
    }

    public void AddEdge(Node from, Node to, int distance)
    {
        from.Neighbors.Add((distance, to));
    }

    public void AddBidirectionalEdge(Node from, Node to, int distance)
    {
        AddEdge(from, to, distance);
        AddEdge(to, from, distance);
    }

    public void TopologicalSort(Node node, HashSet<Node> visited, Stack<Node> stack)
    {
        visited.Add(node);

        foreach (var (_, neighbor) in node.Neighbors)
        {
            if (!visited.Contains(neighbor))
            {
                TopologicalSort(neighbor, visited, stack);
            }
        }

        stack.Push(node);
    }
}
