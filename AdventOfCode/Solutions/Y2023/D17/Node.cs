using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D17;

[DebuggerDisplay("{Position} | {RemainingStraights} | ({Direction})")]
public class Node : IComparable<Node>
{
    public Node Parent { get; set; } = null!;
    public Node[] Children { get; set; } = new Node[0];

    public Direction Direction { get; set; }

    public byte RemainingStraights { get; set; }

    public Coordinate Position { get; set; }

    public byte Cost { get; set; }

    public int G { get; set; }
    public int H { get; set; }
    public int F => G + H;

    public int CompareTo(Node? other)
    {
        return this.F.CompareTo(other?.F);
    }

    public void Expand(
        PriorityQueue<Node, int> openList,
        HashSet<Node> closedList,
        Coordinate goal,
        byte[,] costs
    )
    {
        closedList.Add(this);
        foreach (var child in this.Children)
        {
            if (closedList.Contains(child))
            {
                continue;
            }

            // Sum up costs between parent and child
            int tentativeG = this.G;
            for (int delta = (int)Position.ManhattanDistance(child.Position); delta > 0; delta--)
            {
                Coordinate coordinate = Position + child.Direction.ToCoordinate() * delta;
                tentativeG += costs[coordinate.X, coordinate.Y];
            }

            int tentativeH = (int)child.Position.ManhattanDistance(goal);

            if (
                openList.UnorderedItems.Any(element => element.Element == child)
                && tentativeG >= child.G
            )
            {
                continue;
            }

            child.Parent = this;
            child.G = tentativeG;
            child.H = tentativeH;

            openList.Enqueue(child, child.F);
        }
    }

    public override int GetHashCode()
    {
        return this.Position.GetHashCode();
    }
}
