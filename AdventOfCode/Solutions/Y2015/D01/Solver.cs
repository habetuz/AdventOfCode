using AdventOfCode;
using AdventOfCode.Solver;
using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solutions.Y2015.D01;

public class Solver : ISolver<string>
{

    public void Parse(string input, IPartSubmitter<string> partSubmitter)
    {
        Thread.Sleep(200);
        partSubmitter.SubmitFull("Test");
    }

    public void Solve(string input, IPartSubmitter solution)
    {
        Thread.Sleep(200);
        solution.SubmitFull(5.ToString());
    }
}
