using AdventOfCode;
using AdventOfCode.Solver;
using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solutions.Y2015.D01;

public class Solver : ISolver<string, int>
{

    public void Parse(string input, IPartSubmitter<string, int> partSubmitter)
    {
        Thread.Sleep(200);
        partSubmitter.SubmitPart2(2);
        partSubmitter.SubmitPart1("test");
    }

    public void Solve(string? input1, int input2, IPartSubmitter partSubmitter)
    {
        Thread.Sleep(200);
        partSubmitter.SubmitFull("3");
    }
}
