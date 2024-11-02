using System.Diagnostics;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D23;

[DebuggerDisplay("{Position}")]
public class Node
{
    public Coordinate Position { get; set; }

    public Node? Parent { get; set; }

    public int DistanceFromGoal { get; set; }

    public HashSet<(int distance, Node node)> Neighbors { get; set; } = [];
}
