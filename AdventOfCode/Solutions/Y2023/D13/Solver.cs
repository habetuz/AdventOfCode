using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D13;

public class Solver : ISolver<bool[][,]>
{
    public void Parse(string input, IPartSubmitter<bool[][,]> partSubmitter)
    {
        var blocks = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
        var parsedBlocks = blocks.Select(
            block => Array2D.FromString<bool>(block, (c, x, y) => c == '#')
        );
        partSubmitter.Submit(parsedBlocks.ToArray());
    }

    public void Solve(bool[][,] input, IPartSubmitter partSubmitter)
    {
        long resultWithoutError = 0;
        long resultWithError = 0;

        foreach (var block in input)
        {
            int withoutError = -1;
            int withError = -1;

            // Check for vertical Mirror
            for (int x = 0; x < block.GetLength(0) - 1; x++)
            {
                byte error = CheckForVerticalMirror(block, x, 1);
                if (error == 0)
                {
                    withoutError = x + 1;
                }
                else if (error == 1)
                {
                    withError = x + 1;
                }

                if (withError >= 0 && withoutError >= 0)
                {
                    resultWithoutError += withoutError;
                    resultWithError += withError;
                    break;
                }
            }

            if (withoutError >= 0 && withError >= 0)
            {
                continue;
            }

            // Check for horizontal Mirror
            for (int y = 0; y < block.GetLength(1) - 1; y++)
            {
                byte error = CheckForHorizontalMirror(block, y, 1);
                if (error == 0)
                {
                    withoutError = (y + 1) * 100;
                }
                else if (error == 1)
                {
                    withError = (y + 1) * 100;
                }

                if (withError >= 0 && withoutError >= 0)
                {
                    resultWithoutError += withoutError;
                    resultWithError += withError;
                    break;
                }
            }
        }

        partSubmitter.SubmitPart1(resultWithoutError);
        partSubmitter.SubmitPart2(resultWithError);
    }

    private byte CheckForVerticalMirror(bool[,] block, int x, byte allowedError = 0)
    {
        int mirrorX = x;
        byte consumedError = 0;

        for (int y = 0; y < block.GetLength(1); y++)
        {
            for (x = mirrorX; x >= 0; x--)
            {
                int mirroredX = mirrorX + (mirrorX - x) + 1;
                if (mirroredX >= block.GetLength(0))
                {
                    break;
                }
                if (block[x, y] != block[mirroredX, y])
                {
                    if (consumedError < allowedError)
                    {
                        consumedError++;
                        continue;
                    }
                    return byte.MaxValue;
                }
            }
        }
        return consumedError;
    }

    private byte CheckForHorizontalMirror(bool[,] block, int y, byte allowedError = 0)
    {
        int mirrorY = y;
        byte consumedError = 0;

        for (int x = 0; x < block.GetLength(0); x++)
        {
            for (y = mirrorY; y >= 0; y--)
            {
                int mirroredY = mirrorY + (mirrorY - y) + 1;
                if (mirroredY >= block.GetLength(1))
                {
                    break;
                }
                if (block[x, y] != block[x, mirroredY])
                {
                    if (consumedError < allowedError)
                    {
                        consumedError++;
                        continue;
                    }
                    return byte.MaxValue;
                }
            }
        }
        return consumedError;
    }
}
