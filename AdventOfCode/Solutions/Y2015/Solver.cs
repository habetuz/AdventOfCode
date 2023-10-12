using AdventOfCode;
using AdventOfCode.Solver;
using AdventOfCode.PartSubmitter;

public class Solver : ISolver<string>
{
    public void Parse(string input, IPartSubmitter<string> parsedInput)
    {
        parsedInput.SubmitFull("test");
    }

    public void Solve(string input, IPartSubmitter solution)
    {
        solution.SubmitFull(input);
    }
}