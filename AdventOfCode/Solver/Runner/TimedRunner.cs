using System.Diagnostics;
using System.Runtime.InteropServices;
using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solver.Runner
{
    public class TimedRunner : ISolverRunner
    {
        private readonly static TimeSpan MaxTime = new TimeSpan(0, 0, 5);
        private readonly static TimeSpan WarmupTime = new TimeSpan(0, 0, 0, 0, 100);

        private readonly ISolver<object, object> solver;
        private readonly string input;

        public TimedRunner(ISolver<object, object> solver, string input)
        {
            this.solver = solver;
            this.input = input;
        }
        public Solution Run()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            }

            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            TimedPartSubmitter<object> parseSubmitter = new TimedPartSubmitter<object>();
            TimedPartSubmitter solutionSubmitter = new();

            parseSubmitter.Start();
            this.solver.Parse(this.input, parseSubmitter);

            solutionSubmitter.Start();
            this.solver.Solve(
                parseSubmitter.Parts.Item1,
                parseSubmitter.Parts.Item2,
                solutionSubmitter);

            return new Solution()
            {
                Parse1 = parseSubmitter.Times.Item1,
                Parse2 = parseSubmitter.Times.Item2,
                Solve1 = solutionSubmitter.Times.Item1,
                Solve2 = solutionSubmitter.Times.Item2,
                Solution1 = solutionSubmitter.Parts.Item1,
                Solution2 = solutionSubmitter.Parts.Item2,
            };
        }
    }
}