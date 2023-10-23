using AdventOfCode.PartSubmitter;

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
            SimplePartSubmitter<object> parsedInputSubmitter = new();
            SimplePartSubmitter solutionSubmitter = new();
            this.solver.Parse(this.input, parsedInputSubmitter);
            this.solver.Solve(parsedInputSubmitter.FirstPart, parsedInputSubmitter.SecondPart, solutionSubmitter);
            return new Solution
            {
                Part1 = solutionSubmitter.FirstPart,
                Part2 = solutionSubmitter.SecondPart,
            };
        }
    }
}