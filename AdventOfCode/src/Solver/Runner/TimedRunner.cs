using System.Diagnostics;
using System.Runtime.InteropServices;
using AdventOfCode.PartSubmitter;
using Spectre.Console;

namespace AdventOfCode.Solver.Runner
{
  public class TimedRunner(ISolver<object, object> solver, string input) : ISolverRunner
  {
    private static readonly TimeSpan MaxTime = new(0, 0, 5);
    private static readonly TimeSpan WarmupTime = new(0, 0, 0, 0, 500);

    private readonly ISolver<object, object> solver = solver;
    private readonly string input = input;

    public Solution Run()
    {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
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
          ProgressTask warmup = ctx.AddTask("Warmup...", true, WarmupTime.Ticks);
          var elapsedSum = new TimeSpan();
          (TimeSpan?, TimeSpan?) parseTime = (null, null);
          (TimeSpan?, TimeSpan?) solveTime = (null, null);
          (object?, object?) parseResult = (null, null);
          (object?, object?) solveResult = (null, null);
          Stopwatch runtime = new();
          runtime.Start();
          do
          {
            TimedPartSubmitter<object> parseSubmitter = new();
            TimedPartSubmitter solutionSubmitter = new();
            parseSubmitter.Start();
            solutionSubmitter.Start();
            solver.Parse(input, parseSubmitter);

            if (parseSubmitter.Parts.Item1 is null || parseSubmitter.Parts.Item2 is null)
            {
              throw new Exception("Parsing is not fully implemented!");
            }

            solver.Solve(parseSubmitter.Parts.Item1, parseSubmitter.Parts.Item2, solutionSubmitter);
            parseTime = parseSubmitter.Times;
            parseResult = parseSubmitter.Parts;
            solveTime = solutionSubmitter.Times;
            solveResult = solutionSubmitter.Parts;
            warmup.Value = runtime.Elapsed.Ticks;
          } while (runtime.Elapsed < WarmupTime);
          runtime.Stop();
          warmup.StopTask();

          // Real run only if it's fast enough
          if (runtime.Elapsed < MaxTime)
          {
            ProgressTask run = ctx.AddTask("Running...", true, MaxTime.Ticks);
            elapsedSum = new TimeSpan();
            int loopCount = 0;
            List<(TimeSpan?, TimeSpan?)> parseTimes = [];
            List<(TimeSpan?, TimeSpan?)> solveTimes = [];
            runtime.Restart();
            do
            {
              TimedPartSubmitter<object> parseSubmitter = new();
              TimedPartSubmitter solutionSubmitter = new();
              parseSubmitter.Start();
              solutionSubmitter.Start();
              solver.Parse(input, parseSubmitter);

              if (parseSubmitter.Parts.Item1 is null || parseSubmitter.Parts.Item2 is null)
              {
                throw new Exception("Parsing is not fully implemented!");
              }

              solver.Solve(
                parseSubmitter.Parts.Item1,
                parseSubmitter.Parts.Item2,
                solutionSubmitter
              );
              elapsedSum += parseSubmitter.Times.Item1!.Value;
              elapsedSum += parseSubmitter.Times.Item2!.Value;
              parseTimes.Add(parseSubmitter.Times);
              solveTimes.Add(solutionSubmitter.Times);
              loopCount++;

              run.Value = runtime.Elapsed.Ticks;
            } while (runtime.Elapsed < MaxTime);
            run.StopTask();

            // Calculate median
            parseTimes.Sort((a, b) => a.Item1!.Value.CompareTo(b.Item1!.Value));
            var item1 = parseTimes[parseTimes.Count / 2].Item1;
            parseTimes.Sort((a, b) => a.Item2!.Value.CompareTo(b.Item2!.Value));
            var item2 = parseTimes[parseTimes.Count / 2].Item2;
            parseTime = (item1, item2);

            solveTimes.Sort(
              (a, b) =>
                a.Item1 != null && b.Item1 != null ? a.Item1!.Value.CompareTo(b.Item1!.Value) : 0
            );
            item1 = solveTimes[solveTimes.Count / 2].Item1;
            solveTimes.Sort(
              (a, b) =>
                a.Item2 != null && b.Item2 != null ? a.Item2!.Value.CompareTo(b.Item2!.Value) : 0
            );
            item2 = solveTimes[solveTimes.Count / 2].Item2;
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
