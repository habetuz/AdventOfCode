using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using SharpLog;

namespace AdventOfCode.Solutions.Y2023.D18;

public class Solver : ISolver<Line[], Line[]>
{
    public void Parse(string input, IPartSubmitter<Line[], Line[]> partSubmitter)
    {
        var lines = input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select((line) => line.Split(' '));
        var instructions1 = lines
            .Select(
                (line) =>
                {
                    var direction = line[0][0] switch
                    {
                        'U' => Direction.Up,
                        'D' => Direction.Down,
                        'L' => Direction.Left,
                        'R' => Direction.Right,
                        _ => throw new Exception("Invalid direction"),
                    };
                    var length = byte.Parse(line[1]);
                    return new Instruction { Direction = direction, Length = length };
                }
            )
            .ToArray();

        partSubmitter.SubmitPart1(LinkInstructions(instructions1));

        var instructions2 = lines
            .Select(
                (line) =>
                {
                    var hexString = line[2][2..^1];
                    var length = int.Parse(
                        hexString[0..5],
                        System.Globalization.NumberStyles.HexNumber
                    );
                    var direction = hexString[5] switch
                    {
                        '0' => Direction.Right,
                        '1' => Direction.Down,
                        '2' => Direction.Left,
                        '3' => Direction.Up,
                        _ => throw new Exception("Invalid direction"),
                    };

                    return new Instruction { Direction = direction, Length = length };
                }
            )
            .ToArray();

        partSubmitter.SubmitPart2(LinkInstructions(instructions2));
    }

    private Line[] LinkInstructions(Instruction[] instructions)
    {
        Line[] lines = new Line[instructions.Length];
        Coordinate current = new(0, 0);
        for (int i = 0; i < instructions.Length; i++)
        {
            var instruction = instructions[i];
            Coordinate next = current + (instruction.Direction.ToCoordinate() * instruction.Length);
            lines[i] = new Line(current, next);
            current = next;
        }

        return lines;
    }

    public void Solve(Line[] input1, Line[] input2, IPartSubmitter partSubmitter)
    {
        partSubmitter.SubmitPart1(GetArea(input1));
        partSubmitter.SubmitPart2(GetArea(input2));
    }

    private long GetArea(Line[] lines)
    {
        // Implementation of the shoelace formula modified with picks theorem
        long interior = 0;
        ulong boundary = 0;

        foreach (var line in lines)
        {
            interior += checked(line.Start.X * line.End.Y) - checked(line.End.X * line.Start.Y);
            boundary += line.ManhattanLength;
        }

        interior /= 2;
        interior = Math.Abs(interior);

        return interior + ((long)boundary / 2) + 1;
    }
}
