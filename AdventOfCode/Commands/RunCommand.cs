using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Commands.Settings;
using AdventOfCode.Solver;
using AdventOfCode.Solver.Runner;
using AdventOfCode.Time;
using SharpLog;
using Spectre.Console;
using Spectre.Console.Cli;
using YamlDotNet.Core;

namespace AdventOfCode.Commands
{
  public class RunCommand : Command<RunSettings>
  {
    public override int Execute([NotNull] CommandContext context, [NotNull] RunSettings settings)
    {
      var calendarRange = settings.RunRange;

      SolutionStatisticsManager solutionStatisticsManager = new();
      WebResourceManager webResourceManager = new();
      IInputRetriever inputRetriever = new InputManager(webResourceManager);

      AnsiConsole.Write(
        new Rule()
        {
          Title =
            $"[purple]Running puzzle [yellow]{(calendarRange.StartDate == calendarRange.EndDate ? calendarRange.StartDate.ToString() : "[/]from [yellow]" + calendarRange.StartDate.ToString() + "[/] to [yellow]" + calendarRange.EndDate.ToString())}[/][/]",
          Style = "purple",
        }
      );

      foreach (var date in calendarRange)
      {
        try
        {
          AnsiConsole.Write(
            new Rule()
            {
              Title = "[green]Running puzzle [yellow]" + date.ToString() + "[/][/]",
              Style = "green",
            }
          );

          ISolver<object, object> solver = new GenericSolver(date);
          string input = settings.Example.HasValue
            ? inputRetriever.RetrieveExampleInput(date, settings.Example)!
            : inputRetriever.RetrieveInput(date, settings.Example)!;
          Solution? exampleSolution = inputRetriever.RetrieveExampleSolution(
            date,
            settings.Example
          );
          ISolverRunner runner = settings.RunTimed
            ? new TimedRunner(solver, input)
            : new SingleTimeRunner(solver, input);

          Solution solution = runner.Run();

          if (
            !settings.Example.HasValue
            && solution.Solution1 is not null
            && solution.Solution2 is not null
          )
          {
            solutionStatisticsManager.SubmitTimes(solution, date);
          }

          if (!settings.Example.HasValue)
          {
            PrintResult(solution, solutionStatisticsManager.Retrieve(date));
          }
          else
          {
            PrintResult(solution, exampleSolution);
          }
        }
        catch (GenericSolver.SolutionNotImplementedException)
        {
          Logging.LogError("Solution is not implemented!", "RUNNER");
          continue;
        }
        catch (GenericSolver.ISolverNotImplementedException)
        {
          Logging.LogError("Solution needs to implement ISolver<...>.", "RUNNER");
        }
        catch (Exception e)
        {
          Logging.LogError("Solving failed!", "RUNNER", e);
        }
      }

      ReadMeGenerator readMeGenerator = new(solutionStatisticsManager);
      readMeGenerator.Generate();
      readMeGenerator.Save();

      return 1;
    }

    private static void PrintResult(Solution solution, Solution? expectedSolution)
    {
      Logging.LogInfo("Finished execution!", "RUNNER");

      if (
        solution.Parse1 is not null
        || solution.Parse2 is not null
        || solution.Solve1 is not null
        || solution.Solve2 is not null
      )
      {
        var chart = new BreakdownChart().FullSize().ShowPercentage().ShowTags().HideTagValues();

        var executionTime = new TimeSpan(0);

        if (solution.Parse1.HasValue)
        {
          chart.AddItem(
            $"Parse 1: {solution.Parse1.Value:c}",
            solution.Parse1.Value.Ticks,
            Color.DarkBlue
          );
          executionTime += solution.Parse1.Value;
        }

        if (solution.Parse2.HasValue)
        {
          chart.AddItem(
            $"Parse 2: {solution.Parse2.Value:c}",
            solution.Parse2.Value.Ticks,
            Color.SkyBlue1
          );
          executionTime += solution.Parse2.Value;
        }

        if (solution.Solve1.HasValue)
        {
          chart.AddItem(
            $"Solve 1: {solution.Solve1.Value:c}",
            solution.Solve1.Value.Ticks,
            Color.DarkRed
          );
          executionTime += solution.Solve1.Value;
        }

        if (solution.Solve2.HasValue)
        {
          chart.AddItem(
            $"Solve 2: {solution.Solve2.Value:c}",
            solution.Solve2.Value.Ticks,
            Color.IndianRed
          );
          executionTime += solution.Solve2.Value;
        }

        Logging.LogInfo($"Execution time: [yellow]{executionTime:c}[/]", "RUNNER");
        AnsiConsole.Write(new Padder(chart).Padding(0, 1, 0, 0));
      }

      if (
        expectedSolution is null
        || (expectedSolution.Value.Solution1 is null && expectedSolution.Value.Solution2 is null)
      )
      {
        AnsiConsole.Write(
          new Padder(
            new Table()
            {
              Title = new TableTitle("Solutions", "yellow"),
              Expand = true,
              ShowHeaders = false,
              Border = TableBorder.Rounded,
            }
              .AddColumn(new TableColumn("Part").LeftAligned())
              .AddColumn(new TableColumn("Solution").RightAligned())
              .AddRow("Solution 1", $"[yellow]{solution.Solution1}[/]")
              .AddRow("Solution 2", $"[yellow]{solution.Solution2}[/]")
          ).Padding(0, 1)
        );
      }
      else
      {
        var columns = new Columns(
          getSolutionMessagePanel("Solution 1", expectedSolution?.Solution1, solution.Solution1),
          getSolutionMessagePanel("Solution 2", expectedSolution?.Solution2, solution.Solution2)
        );

        AnsiConsole.Write(new Padder(columns).Padding(0, 1));
      }
    }

    private static Panel getSolutionMessagePanel(string name, string? expected, string? actual)
    {
      if (actual is null)
      {
        return new Panel(new Markup(":cross_mark: [white on red]MISSING RESULT[/]"))
          .Header(name)
          .HeaderAlignment(Justify.Left)
          .Border(BoxBorder.Rounded)
          .Expand();
      }
      else if (expected is null)
      {
        return new Panel(
          new Markup($":warning: [yellow]MISSING SOLUTION[/]. Result: [yellow]{actual}[/]")
        )
          .Header(name)
          .HeaderAlignment(Justify.Left)
          .Border(BoxBorder.Rounded)
          .Expand();
      }
      else if (long.TryParse(expected, out _) && long.TryParse(actual, out _))
      {
        long expectedNum = long.Parse(expected);
        long actualNum = long.Parse(actual);

        long diff = actualNum - expectedNum;

        if (diff == 0)
        {
          return new Panel(new Markup($":check_mark_button: [green]{actual}[/]"))
            .Header(name)
            .HeaderAlignment(Justify.Left)
            .Border(BoxBorder.Rounded)
            .Expand();
        }
        else
        {
          var chart = new BarChart()
            .AddItem("Expected", expectedNum, Color.Green)
            .AddItem("Actual", actualNum, Color.Red);

          var rows = new Rows(
            new Markup(
              $":cross_mark: Expected: [green]{expected}[/] | Actual: [red]{actual}[/] | Diff: [orange1]{diff}[/]"
            ),
            new Padder(chart).Padding(0, 1, 0, 0)
          );

          return new Panel(new Padder(rows))
            .Header(name)
            .HeaderAlignment(Justify.Left)
            .Border(BoxBorder.Rounded)
            .Expand();
        }
      }
      else
      {
        if (expected == actual)
        {
          return new Panel(new Markup($":check_mark_button: [green]{actual}[/]"))
            .Header(name)
            .HeaderAlignment(Justify.Left)
            .Border(BoxBorder.Rounded)
            .Expand();
        }
        else
        {
          return new Panel(
            new Markup($":cross_mark: Expected: [green]{expected}[/] | Actual: [red]{actual}[/]")
          )
            .Header(name)
            .HeaderAlignment(Justify.Left)
            .Border(BoxBorder.Rounded)
            .Expand();
        }
      }
    }
  }
}
