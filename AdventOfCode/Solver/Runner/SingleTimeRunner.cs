using AdventOfCode.PartSubmitter;
using Spectre.Console;
using YamlDotNet.Core.Tokens;

namespace AdventOfCode.Solver.Runner
{
  internal class SingleTimeRunner : ISolverRunner
  {
    private readonly ISolver<object, object> solver;
    private readonly string input;

    public SingleTimeRunner(ISolver<object, object> solver, string input)
    {
      this.solver = solver;
      this.input = input;
    }

    public Solution Run()
    {
      Solution solution = new();
      AnsiConsole
        .Status()
        .SpinnerStyle("orange1")
        .Spinner(Spinner.Known.BouncingBall)
        .Start(
          "Parsing...",
          ctx =>
          {
            SimplePartSubmitter<object> parsedInputSubmitter = new();
            SimplePartSubmitter solutionSubmitter = new();
            solver.Parse(input, parsedInputSubmitter);
            ctx.Status = "Solving...";
            if (parsedInputSubmitter.FirstPart == null || parsedInputSubmitter.SecondPart == null)
            {
              throw new Exception("Parsing is not complete.");
            }

            solver.Solve(
              parsedInputSubmitter.FirstPart,
              parsedInputSubmitter.SecondPart,
              solutionSubmitter
            );
            solution = new()
            {
              Solution1 = solutionSubmitter.FirstPart is null
                ? null
                : solutionSubmitter.FirstPart.ToString(),
              Solution2 = solutionSubmitter.SecondPart is null
                ? null
                : solutionSubmitter.SecondPart.ToString(),
            };
          }
        );

      return solution;
    }
  }
}
