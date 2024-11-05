using System.ComponentModel;
using AdventOfCode.Time;
using SharpLog;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands.Settings;

public class InitSettings : CommandSettings
{
  [Description("The date you want to initialize. Leave empty for the current date.")]
  [CommandArgument(0, "[date]")]
  public string StringDate { get; init; } = "";

  [Description("Wether you want to overwrite existing files.")]
  [CommandOption("-f|--force")]
  public bool Force { get; init; }


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

    return ValidationResult.Success();
  }
}
