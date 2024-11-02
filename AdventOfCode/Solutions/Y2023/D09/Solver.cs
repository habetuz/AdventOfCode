using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2023.D09;

public class Solver : ISolver<History[]>
{
    public void Parse(string input, IPartSubmitter<History[]> partSubmitter)
    {
        var histories = input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(l => l.Split(' ').Select(int.Parse).ToArray())
            .Select(l => new History(l))
            .ToArray();

        partSubmitter.Submit(histories);
    }

    public void Solve(History[] input, IPartSubmitter partSubmitter)
    {
        int sum = input.Sum((history) => history.PredictForward());
        partSubmitter.SubmitPart1(sum);

        sum = input.Sum((history) => history.PredictBackward());
        partSubmitter.SubmitPart2(sum);
    }
}
