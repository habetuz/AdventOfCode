using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.Marshalling;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2023.D04;

public class Solver : ISolver<Game[]>
{
    public void Parse(string input, IPartSubmitter<Game[]> partSubmitter)
    {
        var lines = input.Split((char[])['\n'], System.StringSplitOptions.RemoveEmptyEntries);
        var games = new Game[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var parts = lines[i].Split(':')[1].Split('|');
            var winningNumbers = parts[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => byte.Parse(x))
                .ToArray();
            var numbers = parts[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => byte.Parse(x))
                .ToArray();
            games[i] = new Game(winningNumbers, numbers);
        }

        partSubmitter.SubmitFull(games);
    }

    public void Solve(Game[] input, IPartSubmitter partSubmitter)
    {
        double sum = 0;
        uint totalInstances = 0;

        for (int i = 0; i < input.Length; i++)
        {
            var game = input[i];
            var winningNumbers = game.WinningNumbers;
            var numbers = game.Numbers;
            var matches = 0;
            for (int j = 0; j < winningNumbers.Length; j++)
            {
                var winningNumber = winningNumbers[j];
                for (int k = 0; k < numbers.Length; k++)
                {
                    var number = numbers[k];
                    if (number == winningNumber)
                    {
                        matches++;
                        break;
                    }
                }
            }

            sum += matches > 0 ? Math.Pow(2, matches - 1) : 0;

            for (int j = i + 1; j - i <= matches && j < input.Length; j++)
            {
                input[j].Instances += game.Instances;
            }

            totalInstances += game.Instances;
        }

        partSubmitter.SubmitPart1(sum);
        partSubmitter.SubmitPart2(totalInstances);

        for (int i = 0; i < input.Length; i++)
        {
            input[i].Instances = 1;
        }
    }
}
