using System.ComponentModel;
using AdventOfCode.Time;
using SharpLog;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands.Settings;

public class InitSettings : CommandSettings
{
  private readonly Dictionary<string, Func<Date, string>> generators =
    new()
    {
      { "none", DefaultSolverGenerator },
      { "custom-grid", CustomGridSplittingSolverGenerator },
      { "grid", GridSplittingSolverGenerator },
      { "custom-lines", CustomLineSplittingSolverGenerator },
      { "lines", LineSplittingSolverGenerator },
      { "unmodified", UnmodifingSolverGenerator },
      { "numbers", NumbersSolverGenerator },
    };

  [Description("The date you want to initialize. Leave empty for the current date.")]
  [CommandArgument(0, "[date]")]
  public string StringDate { get; init; } = "";

  [Description("Wether you want to overwrite existing files.")]
  [CommandOption("-f|--force")]
  public bool Force { get; init; }

  [Description(
    "The type of solver you want to create. Choose from 'none', 'custom-grid', 'grid', 'custom-lines', 'lines', 'unmodified', 'numbers'."
  )]
  [CommandOption("-s|--solver")]
  [DefaultValue("none")]
  public string SolverName { get; init; } = null!;

  public Func<Date, string> Generator { get; private set; } = null!;

  public Date Date { get; private set; }

  public string SolutionPath { get; private set; } = null!;

  public override ValidationResult Validate()
  {
    var result = DateConverter.SingleDateFull(StringDate!, out Date date);
    if (!result.Successful)
    {
      return result;
    }

    Date = date;

    SolutionPath = Path.Join(
      ProjectDir.Get(),
      "src",
      "solutions",
      $"Y{Date.Year}",
      $"D{Date.Day:D2}",
      "Solver.cs"
    );

    if (File.Exists(SolutionPath) && Force)
    {
      Logging.LogInfo($"Deleting existing solution for [yellow]{Date}[/].", "RUNNER");
      File.Delete(SolutionPath);
    }
    else if (File.Exists(SolutionPath) && !Force)
    {
      return ValidationResult.Error(
        $"Solution for date {Date} already exists. Use -f|--force to overwrite the existing solution."
      );
    }

    if (generators.TryGetValue(SolverName, out var generator))
    {
      Generator = generator;
    }
    else
    {
      return ValidationResult.Error($"Solver {SolverName} does not exist!");
    }

    return ValidationResult.Success();
  }

  private static string DefaultSolverGenerator(Date date)
  {
    return @$"
      using AdventOfCode.PartSubmitter;
      using AdventOfCode.Solver;

      namespace AdventOfCode.Solutions.Y{date.Year}.D{date.Day:D2};
      
      public class Solver : ISolver<string> 
      {{
        public void Parse(string input, IPartSubmitter<string> partSubmitter)
        {{
          throw new NotImplementedException();
        }}

        public void Solve(string input, IPartSubmitter partSubmitter)
        {{
          throw new NotImplementedException();
        }}
      }}
    ";
  }

  private static string CustomGridSplittingSolverGenerator(Date date)
  {
    return @$"
      using AdventOfCode.PartSubmitter;
      using AdventOfCode.Solver.Templates;

      namespace AdventOfCode.Solutions.Y{date.Year}.D{date.Day:D2};
      
      public class Solver : CustomGridSplittingSolver<TYPE> 
      {{
        public override TYPE Convert(char value, int x, int y)
        {{
          throw new NotImplementedException();
        }}

        public override void Solve(TYPE[,] input, IPartSubmitter partSubmitter)
        {{
          throw new NotImplementedException();
        }}
      }}
    ";
  }

  private static string GridSplittingSolverGenerator(Date date)
  {
    return @$"
      using AdventOfCode.PartSubmitter;
      using AdventOfCode.Solver.Templates;

      namespace AdventOfCode.Solutions.Y{date.Year}.D{date.Day:D2};
      
      public class Solver : GridSplittingSolver 
      {{
        public override void Solve(char[,] input, IPartSubmitter partSubmitter)
        {{
          throw new NotImplementedException();
        }}
      }}
    ";
  }

  private static string CustomLineSplittingSolverGenerator(Date date)
  {
    return @$"
      using AdventOfCode.PartSubmitter;
      using AdventOfCode.Solver.Templates;

      namespace AdventOfCode.Solutions.Y{date.Year}.D{date.Day:D2};
      
      public class Solver : CustomLineSplittingSolver<TYPE> 
      {{
        public override TYPE Convert(string value)
        {{
          throw new NotImplementedException();
        }}

        public override void Solve(TYPE[] input, IPartSubmitter partSubmitter)
        {{
          throw new NotImplementedException();
        }}
      }}
    ";
  }

  private static string LineSplittingSolverGenerator(Date date)
  {
    return @$"
      using AdventOfCode.PartSubmitter;
      using AdventOfCode.Solver.Templates;

      namespace AdventOfCode.Solutions.Y{date.Year}.D{date.Day:D2};
      
      public class Solver : LineSplittingSolver 
      {{
        public override void Solve(string[] input, IPartSubmitter partSubmitter)
        {{
          throw new NotImplementedException();
        }}
      }}
    ";
  }

  private static string UnmodifingSolverGenerator(Date date)
  {
    return @$"
      using AdventOfCode.PartSubmitter;
      using AdventOfCode.Solver.Templates;

      namespace AdventOfCode.Solutions.Y{date.Year}.D{date.Day:D2};
      
      public class Solver : UnmodifingSolver 
      {{
        public override void Solve(string input, IPartSubmitter partSubmitter)
        {{
          throw new NotImplementedException();
        }}
      }}
    ";
  }

  private static string NumbersSolverGenerator(Date date)
  {
    return @$"
      using AdventOfCode.PartSubmitter;
      using AdventOfCode.Solver.Templates;

      namespace AdventOfCode.Solutions.Y{date.Year}.D{date.Day:D2};
      
      public class Solver : NumbersSolver 
      {{
        public override void Solve(int[] input, IPartSubmitter partSubmitter)
        {{
          throw new NotImplementedException();
        }}
      }}
    ";
  }
}
