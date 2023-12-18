using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using SharpLog;

namespace AdventOfCode.Solutions.Y2023.D18;

public class Solver : ISolver<Line[], Line[]>
{
    public void Parse(string input, IPartSubmitter<Instruction[]> partSubmitter)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var instructions = lines
            .Select(
                (line) =>
                {
                    var split = line.Split(' ');
                    return new Instruction()
                    {
                        Direction = split[0] switch
                        {
                            "U" => Direction.Up,
                            "R" => Direction.Right,
                            "D" => Direction.Down,
                            "L" => Direction.Left,
                            _ => throw new Exception("Invalid direction")
                        },
                        Length = byte.Parse(split[1]),
                        Color = split[2][2..^2]
                    };
                }
            )
            .ToArray();

        partSubmitter.Submit(instructions);
    }

    public void Solve(Instruction[] input, IPartSubmitter partSubmitter)
    {
        Coordinate digger = new Coordinate(0, 0);
        Queue<Coordinate> coordinates = new Queue<Coordinate>(10000);

        int maxX = 0;
        int maxY = 0;
        int minX = 0;
        int minY = 0;

        foreach (Instruction instruction in input)
        {
            for (int i = 0; i < instruction.Length; i++)
            {
                coordinates.Enqueue(digger);
                digger += instruction.Direction;

                if (digger.X > maxX)
                {
                    maxX = digger.X;
                }
                else if (digger.X < minX)
                {
                    minX = digger.X;
                }
                else if (digger.Y > maxY)
                {
                    maxY = digger.Y;
                }
                else if (digger.Y < minY)
                {
                    minY = digger.Y;
                }
            }
        }

        bool[,] lagoon = new bool[maxX - minX + 1, maxY - minY + 1];

        while (coordinates.Count > 0)
        {
            var coordinate = coordinates.Dequeue();
            lagoon[coordinate.X - minX, coordinate.Y - minY] = true;
        }

        //Array2D.Print(lagoon, (value, x, y) => value ? "#" : ".");
        FloodFill(lagoon);

        var lagoonSize = (from bool value in lagoon where value select value).Count();

        partSubmitter.SubmitPart1(lagoonSize);
    }

    public void FloodFill(bool[,] lagoon)
    {
        Queue<Coordinate> queue = new Queue<Coordinate>();
        queue.Enqueue(FindStart(lagoon));

        while (queue.Count > 0)
        {
            var coordinate = queue.Dequeue();

            if (lagoon[coordinate.X, coordinate.Y])
            {
                continue;
            }

            lagoon[coordinate.X, coordinate.Y] = true;

            queue.Enqueue(coordinate + Direction.Up);
            queue.Enqueue(coordinate + Direction.Right);
            queue.Enqueue(coordinate + Direction.Down);
            queue.Enqueue(coordinate + Direction.Left);
        }
    }

    private Coordinate FindStart(bool[,] lagoon)
    {
        for (int x = 1; x < lagoon.GetLength(0); x++)
        {
            for (int y = 0; y < lagoon.GetLength(1); y++)
            {
                if (lagoon[x, y])
                {
                    if (y < lagoon.GetLength(1) - 1 && !lagoon[x, y + 1])
                    {
                        return new Coordinate(x, y + 1);
                    }

                    break;
                }
            }
        }

        throw new Exception("No start found");
    }
}
