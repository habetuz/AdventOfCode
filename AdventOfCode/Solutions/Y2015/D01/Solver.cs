using AdventOfCode;
using AdventOfCode.Solver;
using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solutions.Y2015.D01;

public class Solver : ISolver<string, int>
{

    public void Parse(string input, IPartSubmitter<string, int> partSubmitter)
    {
        Thread.Sleep(200);
        partSubmitter.SubmitPart1("test");
        partSubmitter.SubmitPart2(2);
    }

    public void Solve(string? input1, int input2, IPartSubmitter partSubmitter)
    {
        partSubmitter.SubmitFull("3");
    }
}
