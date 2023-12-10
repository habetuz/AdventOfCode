using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D10;

public class Solver : ISolver<(Direction[,], Coordinate)>
{
    public void Parse(string input, IPartSubmitter<(Direction[,], Coordinate)> partSubmitter)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var start = new Coordinate();
        var map = Array2D.FromString<Direction>(
            input,
            (c, x, y) =>
            {
                if (c == 'S')
                {
                    start = new Coordinate(x, y);
                }
                return c switch
                {
                    '|' => Direction.Up | Direction.Down,
                    '-' => Direction.Left | Direction.Right,
                    'L' => Direction.Up | Direction.Right,
                    'J' => Direction.Up | Direction.Left,
                    '7' => Direction.Left | Direction.Down,
                    'F' => Direction.Down | Direction.Right,
                    '.' => Direction.None,
                    'S' => Direction.None,
                    _ => throw new InvalidDataException("Char not supported")
                };
            }
        );

        Array2D.IterateAroundCoordinate(
            map,
            start.X,
            start.Y,
            (array, x, y, direction) =>
            {
                if ((direction & map[x, y].Invert()) != 0)
                {
                    map[start.X, start.Y] |= direction;
                }

                return Direction.None;
            },
            Direction.UpLeft | Direction.UpRight | Direction.DownRight | Direction.DownLeft
        );

        partSubmitter.SubmitFull((map, start));
    }

    public void Solve((Direction[,], Coordinate) input, IPartSubmitter partSubmitter)
    {
        var map = input.Item1;
        var pipeMap = new Direction[map.GetLength(0), map.GetLength(0)];
        var start = input.Item2;
        var previous = input.Item2;
        var position = input.Item2;
        uint length = 0;

        do
        {
            foreach (var direction in map[position.X, position.Y].Iterate())
            {
                var next = position + direction;
                if (next != previous)
                {
                    if (position - previous == Direction.Up || position - previous == Direction.Down)
                    {
                        pipeMap[position.X, position.Y] = position - previous;
                    }
                    else if (next - position == Direction.Up || next - position == Direction.Down)
                    {
                        pipeMap[position.X, position.Y] = next - position;
                    }
                    else
                    {
                        pipeMap[position.X, position.Y] = Direction.Left | Direction.Right;
                    }

                    previous = position;
                    position = next;
                    break;
                }
            }

            length++;
        } while (position != start);

        // Special case for start pipe
        pipeMap[position.X, position.Y] = position - previous;

        partSubmitter.SubmitPart1(length / 2);

        int inboundCount = 0;
        for (int y = 0; y < pipeMap.GetLength(1); y++)
        {
            bool inside = false;
            Direction lastDirection = Direction.None;
            for (int x = 0; x < pipeMap.GetLength(0); x++)
            {
                if (pipeMap[x, y] != Direction.None)
                {
                    if (
                        !pipeMap[x, y].HasFlag(Direction.Right)
                        && !pipeMap[x, y].HasFlag(Direction.Left)
                        && pipeMap[x, y] != lastDirection
                    )
                    {
                        inside = !inside;
                        lastDirection = pipeMap[x, y];
                    }
                }
                else if (inside)
                {
                    inboundCount++;
                }
            }
        }

        partSubmitter.SubmitPart2(inboundCount);
    }
}
