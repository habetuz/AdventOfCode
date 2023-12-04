using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2015.D03;

public class Solver: ISolver<string>
{
    public void Parse(string input, IPartSubmitter<string> partSubmitter)
    {
        partSubmitter.SubmitFull(input);
    }

    public void Solve(string input, IPartSubmitter partSubmitter)
    {
        var visited = new HashSet<Coordinate>();
        var santa = new Coordinate(0, 0);
        var roboSanta = new Coordinate(0, 0);
        visited.Add(santa);

        for (int i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case '^':
                    santa.Y++;
                    break;
                case 'v':
                    santa.Y--;
                    break;
                case '>':
                    santa.X++;
                    break;
                case '<':
                    santa.X--;
                    break;
            }

            visited.Add(santa);
        }

        partSubmitter.SubmitPart1(visited.Count);

        visited.Clear();
        visited.Add(santa);
        for (int i = 0; i < input.Length; i++)
        {
            var current = i % 2 == 0 ? santa : roboSanta;
            switch (input[i])
            {
                case '^':
                    current.Y++;
                    break;
                case 'v':
                    current.Y--;
                    break;
                case '>':
                    current.X++;
                    break;
                case '<':
                    current.X--;
                    break;
            }

            visited.Add(current);
        }

        partSubmitter.SubmitPart2(visited.Count);
    }
}