using System.Diagnostics;
using System.Runtime.InteropServices;
using AdventOfCode.PartSubmitter;
using SharpLog;
using Spectre.Console;

namespace AdventOfCode.Solver.Runner
{
    public class TimedRunner : ISolverRunner
    {
        private static readonly TimeSpan MaxTime = new TimeSpan(0, 0, 5);
        private static readonly TimeSpan WarmupTime = new TimeSpan(0, 0, 0, 0, 100);

        private static readonly int MaxLoopCount = 300;

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

            Solution solution = new();
            AnsiConsole
                .Progress()
                .HideCompleted(true)
                .Start(ctx =>
                {
                    // Warmup
                    ProgressTask warmupParsing = ctx.AddTask(
                        "Warmup parsing...",
                        true,
                        WarmupTime.Ticks
                    );
                    var elapsedSum = new TimeSpan();
                    var loopCount = 0;
                    (TimeSpan?, TimeSpan?) parseTime = (null, null);
                    (object?, object?) parseResult = (null, null);
                    do
                    {
                        TimedPartSubmitter<object> parseSubmitter =
                            new TimedPartSubmitter<object>();
                        parseSubmitter.Start();
                        this.solver.Parse(this.input, parseSubmitter);
                        elapsedSum += parseSubmitter.Times.Item1!.Value;
                        elapsedSum += parseSubmitter.Times.Item2!.Value;
                        parseTime = parseSubmitter.Times;
                        parseResult = parseSubmitter.Parts;
                        warmupParsing.Value = elapsedSum.Ticks;
                    } while (elapsedSum < WarmupTime);
                    warmupParsing.StopTask();

                    // Real run only if it's fast enough
                    if (elapsedSum < MaxTime)
                    {
                        ProgressTask parsing = ctx.AddTask("Parsing...");
                        elapsedSum = new TimeSpan();
                        List<(TimeSpan?, TimeSpan?)> parseTimes = new();
                        do
                        {
                            TimedPartSubmitter<object> parseSubmitter =
                                new TimedPartSubmitter<object>();
                            parseSubmitter.Start();
                            this.solver.Parse(this.input, parseSubmitter);
                            elapsedSum += parseSubmitter.Times.Item1!.Value;
                            elapsedSum += parseSubmitter.Times.Item2!.Value;
                            parseTimes.Add(parseSubmitter.Times);
                            loopCount++;

                            var timeProgress = elapsedSum / MaxTime * 100;
                            var loopProgress = (double)loopCount / MaxLoopCount * 100;
                            parsing.Value =
                                timeProgress > loopProgress ? timeProgress : loopProgress;
                        } while (elapsedSum < MaxTime && loopCount < MaxLoopCount);
                        parsing.StopTask();

                        parseTimes.Sort((a, b) => a.Item1!.Value.CompareTo(b.Item1!.Value));
                        var item1 = parseTimes[parseTimes.Count / 2].Item1;
                        parseTimes.Sort((a, b) => a.Item2!.Value.CompareTo(b.Item2!.Value));
                        var item2 = parseTimes[parseTimes.Count / 2].Item2;
                        parseTime = (item1, item2);
                    }

                    if (parseResult.Item1 is null || parseResult.Item2 is null)
                    {
                        throw new Exception("Parsing is not implemented!");
                    }

                    // Warmup
                    ProgressTask warmupSolving = ctx.AddTask(
                        "Warmup solving...",
                        true,
                        WarmupTime.Ticks
                    );
                    elapsedSum = new TimeSpan();
                    loopCount = 0;
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
                        warmupSolving.Value = elapsedSum.Ticks;
                    } while (elapsedSum < WarmupTime);
                    warmupSolving.StopTask();

                    // Real run only if it's fast enough
                    if (elapsedSum < MaxTime)
                    {
                        ProgressTask solving = ctx.AddTask("Solving...");
                        elapsedSum = new TimeSpan();
                        List<(TimeSpan?, TimeSpan?)> solveTimes = new();
                        do
                        {
                            TimedPartSubmitter solutionSubmitter = new();
                            solutionSubmitter.Start();
                            this.solver.Solve(
                                parseResult.Item1,
                                parseResult.Item2,
                                solutionSubmitter
                            );
                            elapsedSum += solutionSubmitter.Times.Item1!.Value;
                            elapsedSum += solutionSubmitter.Times.Item2!.Value;
                            solveTimes.Add(solutionSubmitter.Times);
                            loopCount++;

                            var timeProgress = elapsedSum / MaxTime * 100;
                            var loopProgress = (double)loopCount / MaxLoopCount * 100;
                            solving.Value =
                                timeProgress > loopProgress ? timeProgress : loopProgress;
                        } while (elapsedSum < MaxTime && loopCount < MaxLoopCount);
                        solving.StopTask();

                        solveTimes.Sort((a, b) => a.Item1!.Value.CompareTo(b.Item1!.Value));
                        var item1 = solveTimes[solveTimes.Count / 2].Item1;
                        solveTimes.Sort((a, b) => a.Item2!.Value.CompareTo(b.Item2!.Value));
                        var item2 = solveTimes[solveTimes.Count / 2].Item2;
                        solveTime = (item1, item2);
                    }

                    solution = new()
                    {
                        Parse1 = parseTime.Item1,
                        Parse2 = parseTime.Item2,
                        Solve1 = solveTime.Item1,
                        Solve2 = solveTime.Item2,
                        Solution1 = solveResult.Item1?.ToString(),
                        Solution2 = solveResult.Item2?.ToString(),
                    };
                });

            return solution;
        }
    }
}
