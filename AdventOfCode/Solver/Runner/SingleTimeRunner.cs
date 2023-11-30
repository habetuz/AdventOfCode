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
                        this.solver.Parse(this.input, parsedInputSubmitter);
                        ctx.Status = "Solving...";
                        this.solver.Solve(
                            parsedInputSubmitter.FirstPart,
                            parsedInputSubmitter.SecondPart,
                            solutionSubmitter
                        );
                        return new Solution
                        {
                            Solution1 = solutionSubmitter.FirstPart.ToString(),
                            Solution2 = solutionSubmitter.SecondPart.ToString(),
                        };
                    }
                );

            throw new Exception("This should never happen.");
        }
    }
}
