using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using SharpLog;

namespace AdventOfCode.Solutions.Y2023.D11;

public class Solver : ISolver<Coordinate[], Coordinate[]>
{
    public void Parse(string input, IPartSubmitter<Coordinate[], Coordinate[]> partSubmitter)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        byte[] shiftX = new byte[lines[0].Length];
        byte[] shiftY = new byte[lines.Length];
        List<Coordinate> galaxies = [];

        // Scan vertically
        byte currentShift = 0;
        for (int y = 0; y < shiftY.Length; y++)
        {
            bool emptyLine = true;
            for (int x = 0; x < shiftX.Length; x++)
            {
                if (lines[y][x] == '#')
                {
                    galaxies.Add(new Coordinate(x, y));
                    emptyLine = false;
                }
            }

            shiftY[y] = currentShift;

            if (emptyLine)
            {
                currentShift++;
            }
        }

        // Scan horizontally
        currentShift = 0;
        for (int x = 0; x < shiftX.Length; x++)
        {
            bool emptyLine = true;
            for (int y = 0; y < shiftY.Length; y++)
            {
                if (lines[y][x] == '#')
                {
                    emptyLine = false;
                }
            }

            shiftX[x] = currentShift;

            if (emptyLine)
            {
                currentShift++;
            }
        }

        partSubmitter.SubmitPart1(
            galaxies
                .Select((galaxy) => galaxy + new Coordinate(shiftX[galaxy.X], shiftY[galaxy.Y]))
                .ToArray()
        );
        partSubmitter.SubmitPart2(
            galaxies
                .Select(
                    (galaxy) =>
                        galaxy
                        + ((1_000_000 - 1) * new Coordinate(shiftX[galaxy.X], shiftY[galaxy.Y]))
                )
                .ToArray()
        );
    }

    public void Solve(Coordinate[] input1, Coordinate[] input2, IPartSubmitter partSubmitter)
    {
        ulong sum1 = 0;
        ulong sum2 = 0;
        for (int i = 0; i < input1.Length; i++)
        {
            for (int j = i + 1; j < input1.Length; j++)
            {
                sum1 += input1[i].ManhattanDistance(input1[j]);
                sum2 += input2[i].ManhattanDistance(input2[j]);
            }
        }

        partSubmitter.SubmitPart1(sum1);
        partSubmitter.SubmitPart2(sum2);
    }
}
