using AdventOfCode;
using AdventOfCode.Solver;
using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solutions.Y2015.D01;

public class Solver : ISolver<string>
{

    public void Parse(string input, IPartSubmitter<string> partSubmitter)
    {
        partSubmitter.SubmitFull("Test");
    }

    public void Solve(string input, IPartSubmitter solution)
    {
        solution.SubmitFull(input);
    }
}
