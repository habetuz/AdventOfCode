using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2023.D02;

public class Solver : ISolver<Game[]>
{
    const byte MAX_RED = 12;
    const byte MAX_GREEN = 13;
    const byte MAX_BLUE = 14;

    public void Parse(string input, IPartSubmitter<Game[]> partSubmitter)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        var games = new Game[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(":");

            var subsetStrings = parts[1].Split(";");

            var subsets = new Subset[subsetStrings.Length];

            for (int k = 0; k < subsetStrings.Length; k++)
            {
                var colorStrings = subsetStrings[k].Split(",", StringSplitOptions.TrimEntries);

                for (int y = 0; y < colorStrings.Length; y++)
                {
                    var number = byte.Parse(colorStrings[y][0..2]);
                    switch (colorStrings[y][^1])
                    {
                        case 'd': // red
                            subsets[k].Red = number;
                            break;
                        case 'n': // green
                            subsets[k].Green = number;
                            break;
                        case 'e': // blue
                            subsets[k].Blue = number;
                            break;
                    }
                };
            }

            games[i].Subsets = subsets;
        }

        partSubmitter.Submit(games);
    }

    public void Solve(Game[] input, IPartSubmitter partSubmitter)
    {
        var possibleGamesSum = 0;
        uint powerSum = 0;

        for (int i = 0; i < input.Length; i++)
        {
            var game = input[i];
            var subsets = game.Subsets;

            var valide = true;

            var minRed = 0;
            var minGreen = 0;
            var minBlue = 0;

            for (int k = 0; k < subsets.Length; k++)
            {
                var subset = subsets[k];
                if (
                    subsets[k].Red > MAX_RED
                    || subsets[k].Green > MAX_GREEN
                    || subsets[k].Blue > MAX_BLUE
                )
                {
                    valide = false;
                }

                if (subset.Red > minRed)
                {
                    minRed = subset.Red;
                }

                if (subset.Green > minGreen)
                {
                    minGreen = subset.Green;
                }

                if (subset.Blue > minBlue)
                {
                    minBlue = subset.Blue;
                }
            }

            if (valide)
            {
                possibleGamesSum += i + 1;
            }

            powerSum += (uint)minRed * (uint)minGreen * (uint)minBlue;
        }

        partSubmitter.SubmitPart1(possibleGamesSum);
        partSubmitter.SubmitPart2(powerSum);
    }
}
