using System.Diagnostics;
using System.Runtime.InteropServices;
using AdventOfCode.PartSubmitter;
using SharpLog;

namespace AdventOfCode.Solver.Runner
{
    public class TimedRunner : ISolverRunner
    {
        private static readonly TimeSpan MaxTime = new TimeSpan(0, 0, 5);
        private static readonly TimeSpan WarmupTime = new TimeSpan(0, 0, 0, 0, 100);

        private readonly ISolver<object, object> solver;
        private readonly string input;

        public TimedRunner(ISolver<object, object> solver, string input)
        {
            this.solver = solver;
            this.input = input;
        }

        public Solution Run()
        {
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                || RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
            )
            {
                Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            }

            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            // TimedPartSubmitter<object> parseSubmitter = new TimedPartSubmitter<object>();
            // TimedPartSubmitter solutionSubmitter = new();

            // parseSubmitter.Start();
            // this.solver.Parse(this.input, parseSubmitter);

            // solutionSubmitter.Start();
            // this.solver.Solve(
            //     parseSubmitter.Parts.Item1,
            //     parseSubmitter.Parts.Item2,
            //     solutionSubmitter
            // );

            // Warmup
            var elapsedSum = new TimeSpan();
            (TimeSpan?, TimeSpan?) parseTime = (null, null);
            (object?, object?) parseResult = (null, null);
            do
            {
                TimedPartSubmitter<object> parseSubmitter = new TimedPartSubmitter<object>();
                parseSubmitter.Start();
                this.solver.Parse(this.input, parseSubmitter);
                elapsedSum += parseSubmitter.Times.Item1!.Value;
                elapsedSum += parseSubmitter.Times.Item2!.Value;
                parseTime = parseSubmitter.Times;
                parseResult = parseSubmitter.Parts;
            } while (elapsedSum < WarmupTime);

            // Real run only if it's fast enough
            if (elapsedSum < MaxTime)
            {
                elapsedSum = new TimeSpan();
                List<(TimeSpan?, TimeSpan?)> parseTimes = new();
                do
                {
                    TimedPartSubmitter<object> parseSubmitter = new TimedPartSubmitter<object>();
                    parseSubmitter.Start();
                    this.solver.Parse(this.input, parseSubmitter);
                    elapsedSum += parseSubmitter.Times.Item1!.Value;
                    elapsedSum += parseSubmitter.Times.Item2!.Value;
                    Logging.LogDebug(
                        $"parse: {elapsedSum} - {parseSubmitter.Times.Item1!.Value + parseSubmitter.Times.Item2!.Value}",
                        "RUNNER"
                    );
                    parseTimes.Add(parseSubmitter.Times);
                } while (elapsedSum < MaxTime);

                parseTimes.Sort((a, b) => a.Item1!.Value.CompareTo(b.Item1!.Value));
                var item1 = parseTimes[parseTimes.Count / 2].Item1;
                parseTimes.Sort((a, b) => a.Item2!.Value.CompareTo(b.Item2!.Value));
                var item2 = parseTimes[parseTimes.Count / 2].Item2;
                parseTime = (item1, item2);
            }

            // Warmup
            elapsedSum = new TimeSpan();
            (TimeSpan?, TimeSpan?) solveTime = (null, null);
            (object?, object?) solveResult = (null, null);
            do
            {
                TimedPartSubmitter solutionSubmitter = new();
                solutionSubmitter.Start();
                this.solver.Solve(parseResult.Item1, parseResult.Item2, solutionSubmitter);
                elapsedSum += solutionSubmitter.Times.Item1!.Value;
                elapsedSum += solutionSubmitter.Times.Item2!.Value;
                solveTime = solutionSubmitter.Times;
                solveResult = solutionSubmitter.Parts;
            } while (elapsedSum < WarmupTime);

            // Real run only if it's fast enough
            if (elapsedSum < MaxTime)
            {
                elapsedSum = new TimeSpan();
                List<(TimeSpan?, TimeSpan?)> solveTimes = new();
                do
                {
                    TimedPartSubmitter solutionSubmitter = new();
                    solutionSubmitter.Start();
                    this.solver.Solve(parseResult.Item1, parseResult.Item2, solutionSubmitter);
                    elapsedSum += solutionSubmitter.Times.Item1!.Value;
                    elapsedSum += solutionSubmitter.Times.Item2!.Value;
                    Logging.LogDebug(
                        $"solve: {elapsedSum} - {solutionSubmitter.Times.Item1!.Value + solutionSubmitter.Times.Item2!.Value}",
                        "RUNNER"
                    );
                    solveTimes.Add(solutionSubmitter.Times);
                } while (elapsedSum < MaxTime);

                solveTimes.Sort((a, b) => a.Item1!.Value.CompareTo(b.Item1!.Value));
                var item1 = solveTimes[solveTimes.Count / 2].Item1;
                solveTimes.Sort((a, b) => a.Item2!.Value.CompareTo(b.Item2!.Value));
                var item2 = solveTimes[solveTimes.Count / 2].Item2;
                solveTime = (item1, item2);
            }

            return new Solution()
            {
                Parse1 = parseTime.Item1,
                Parse2 = parseTime.Item2,
                Solve1 = solveTime.Item1,
                Solve2 = solveTime.Item2,
                Solution1 = solveResult.Item1?.ToString(),
                Solution2 = solveResult.Item2?.ToString(),
            };
        }
    }
}
