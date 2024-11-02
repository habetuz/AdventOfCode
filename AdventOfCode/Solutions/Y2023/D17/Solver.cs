using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using SharpLog;

namespace AdventOfCode.Solutions.Y2023.D17;

// TODO: Optimize by reducing the amount of links between nodes
// Example: If a node has 3 straights left, it should only be linked to nodes with 2 straights left.
// FIXME: This solution currently produces the wrong solutions for puzzle 2.
public class Solver : ISolver<Node[,,,], Node[,,,]>
{
    public void Parse(string input, IPartSubmitter<Node[,,,], Node[,,,]> partSubmitter)
    {
        partSubmitter.SubmitPart1(Parse(input, 1, 3));
        partSubmitter.SubmitPart2(Parse(input, 4, 10));
    }

    private Node[,,,] Parse(string input, byte minStraight, byte maxStraight)
    {
        var lines = input.Split((char[])['\n'], StringSplitOptions.RemoveEmptyEntries);
        Node[,,,] nodes = new Node[lines[0].Length, lines.Length, maxStraight, 4];

        // Initialize nodes
        for (byte z = 0; z < nodes.GetLength(2); z++)
        {
            for (int y = 0; y < nodes.GetLength(1); y++)
            {
                for (int x = 0; x < nodes.GetLength(0); x++)
                {
                    nodes[x, y, z, 0] = new Node
                    {
                        Position = new Coordinate(x, y),
                        Cost = (byte)(lines[y][x] - '0'),
                        Direction = Direction.Up,
                        RemainingStraights = z,
                    };

                    nodes[x, y, z, 1] = new Node
                    {
                        Position = new Coordinate(x, y),
                        Cost = (byte)(lines[y][x] - '0'),
                        Direction = Direction.Right,
                        RemainingStraights = z,
                    };

                    nodes[x, y, z, 2] = new Node
                    {
                        Position = new Coordinate(x, y),
                        Cost = (byte)(lines[y][x] - '0'),
                        Direction = Direction.Down,
                        RemainingStraights = z,
                    };

                    nodes[x, y, z, 3] = new Node
                    {
                        Position = new Coordinate(x, y),
                        Cost = (byte)(lines[y][x] - '0'),
                        Direction = Direction.Left,
                        RemainingStraights = z,
                    };
                }
            }
        }

        // Modify start node
        for (
            int availableStraights = 0;
            availableStraights < nodes.GetLength(2);
            availableStraights++
        )
        {
            for (int i = 0; i < 4; i++)
            {
                nodes[0, 0, availableStraights, i].Direction = Direction.None;
            }
        }

        // Link nodes
        for (
            int availableStraights = 0;
            availableStraights < nodes.GetLength(2);
            availableStraights++
        )
        {
            for (int y = 0; y < nodes.GetLength(1); y++)
            {
                for (int x = 0; x < nodes.GetLength(0); x++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var node = nodes[x, y, availableStraights, i];
                        List<Node> children = [];
                        foreach (
                            Direction direction in (
                                Direction.Up | Direction.Right | Direction.Down | Direction.Left
                            ).Iterate()
                        )
                        {
                            if (node.Direction.Invert() == direction)
                            {
                                continue;
                            }

                            if (node.Direction == direction)
                            {
                                if (availableStraights == 0)
                                {
                                    continue;
                                }

                                // We are going straight
                                Coordinate offset = direction.ToCoordinate();
                                if (
                                    x + offset.X < 0
                                    || x + offset.X >= nodes.GetLength(0)
                                    || y + offset.Y < 0
                                    || y + offset.Y >= nodes.GetLength(1)
                                )
                                {
                                    continue;
                                }

                                children.Add(
                                    nodes[
                                        x + offset.X,
                                        y + offset.Y,
                                        availableStraights - 1,
                                        DirectionIndex(direction)
                                    ]
                                );
                            }
                            else
                            {
                                // We are turning
                                Coordinate offset = direction.ToCoordinate() * minStraight;
                                if (
                                    x + offset.X < 0
                                    || x + offset.X >= nodes.GetLength(0)
                                    || y + offset.Y < 0
                                    || y + offset.Y >= nodes.GetLength(1)
                                )
                                {
                                    continue;
                                }

                                children.Add(
                                    nodes[
                                        x + offset.X,
                                        y + offset.Y,
                                        maxStraight - minStraight,
                                        DirectionIndex(direction)
                                    ]
                                );
                            }
                        }

                        node.Children = children.ToArray();
                    }
                }
            }
        }

        return nodes;
    }

    public void Solve(Node[,,,] input1, Node[,,,] input2, IPartSubmitter partSubmitter)
    {
        partSubmitter.SubmitPart1(FindPath(input1));
        partSubmitter.SubmitPart2(FindPath(input2));
    }

    private int FindPath(Node[,,,] nodes)
    {
        // Calculate costs
        byte[,] costs = new byte[nodes.GetLength(0), nodes.GetLength(1)];
        for (int y = 0; y < nodes.GetLength(1); y++)
        {
            for (int x = 0; x < nodes.GetLength(0); x++)
            {
                costs[x, y] = nodes[x, y, 0, 0].Cost;
            }
        }

        PriorityQueue<Node, int> openList = new();
        HashSet<Node> closedList = [];
        Coordinate goal = new(nodes.GetLength(0) - 1, nodes.GetLength(1) - 1);
        Node start = nodes[0, 0, 2, 0];
        start.H = (int)start.Position.ManhattanDistance(goal);
        Node goalNode = null!;

        openList.Enqueue(start, start.F);

        int lowestH = int.MaxValue;

        while (openList.Count > 0)
        {
            Node current = openList.Dequeue();

            if (current.H < lowestH)
            {
                lowestH = current.H;
                // Logging.LogDebug(lowestH);
            }

            if (current.Position == goal)
            {
                goalNode = current;

                // // PrintPath(goalNode);
                // Direction[,] path = new Direction[nodes.GetLength(0), nodes.GetLength(1)];
                // Node? currentPathNode = goalNode;
                // while (currentPathNode != null)
                // {
                //     path[currentPathNode.Position.X, currentPathNode.Position.Y] =
                //         currentPathNode.Direction;
                //     currentPathNode = currentPathNode.Parent;
                // }

                // Array2D.Print(
                //     path,
                //     (direction, x, y) =>
                //         direction switch
                //         {
                //             Direction.Up => "↑",
                //             Direction.Right => "→",
                //             Direction.Down => "↓",
                //             Direction.Left => "←",
                //             _ => ".",
                //         }
                // );

                return current.G;
            }

            current.Expand(openList, closedList, goal, costs);
        }

        throw new Exception("No path found");
    }

    private static int DirectionIndex(Direction direction)
    {
        return direction switch
        {
            Direction.Up => 0,
            Direction.Right => 1,
            Direction.Down => 2,
            Direction.Left => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
        };
    }
}
