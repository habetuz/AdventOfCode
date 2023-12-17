using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using SharpLog;
using Spectre.Console;

namespace AdventOfCode.Solutions.Y2023.D17;

public class Solver : ISolver<Node[,]>
{
    public void Parse(string input, IPartSubmitter<Node[,]> partSubmitter)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        partSubmitter.Submit(
            Array2D.FromString<Node>(
                input,
                (c, x, y) => new Node { Value = (byte)(c - '0'), Position = new Coordinate(x, y) }
            )
        );
    }

    public void Solve(Node[,] input, IPartSubmitter partSubmitter)
    {
        Coordinate goal = new(input.GetLength(0) - 1, input.GetLength(1) - 1);

        SortedList<int, Node> openList = new();
        input[0, 0].G = input[0, 0].Cost;
        input[0, 0].H = (int)input[0, 0].Position.ManhattanDistance(goal);
        openList.Add(0, input[0, 0]);

        while (openList.Count > 0)
        {
            Node current = openList.First().Value;
            openList.RemoveAt(0);

            if (current.Position == goal)
            {
                partSubmitter.Submit(current.G);
                break;
            }

            foreach (
                Direction direction in (
                    Direction.Up | Direction.Down | Direction.Left | Direction.Right
                ).Iterate()
            )
            {
                Coordinate newPosition = current.Position + direction;
                if (
                    newPosition.X < 0
                    || newPosition.Y < 0
                    || newPosition.X >= input.GetLength(0)
                    || newPosition.Y >= input.GetLength(1)
                    || (direction == current.EnteringDirection && current.StraightCount > 3)
                )
                {
                    continue;
                }

                Node newNode = input[newPosition.X, newPosition.Y];

                int newG = current.G + newNode.Cost;
                byte newStraightCount = (byte)(
                    current.EnteringDirection == direction ? current.StraightCount + 1 : 0
                );
                if (newNode.G == 0 || newG < newNode.G || newStraightCount < newNode.StraightCount)
                {
                    newNode.G = newG;
                    newNode.H = (int)newNode.Position.ManhattanDistance(goal);
                    newNode.EnteringDirection = direction;
                    newNode.StraightCount = newStraightCount;
                    openList[newNode.F] = newNode;
                }
            }

            AnsiConsole.Write(
                new Rule($"[bold red]New Step. Open list: [yellow]{openList.Count}[/][/]")
            );
            Array2D.Print(
                input,
                (n, x, y) =>
                {
                    var str = n.EnteringDirection switch
                    {
                        Direction.Up => "↑",
                        Direction.Down => "↓",
                        Direction.Left => "←",
                        Direction.Right => "→",
                        Direction.None => ".",
                        _ => n.Value.ToString()
                    };

                    if (openList.ContainsValue(n))
                    {
                        str = $"[yellow]{str}[/]";
                    }
                    else if (n.G > 0)
                    {
                        str = $"[green]{str}[/]";
                    }

                    return str;
                }
            );
        }
    }
}
