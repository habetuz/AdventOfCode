using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands.Settings;

public class SetReadmeFileSettings : CommandSettings
{
  [Description(
    "The path to your README.md file. It will be updated as you solve puzzles. Leave empty to remove the set README.md file path."
  )]
  [CommandArgument(0, "[path]")]
  public string? Path { get; init; } = null!;

  public override ValidationResult Validate()
  {
    if (string.IsNullOrEmpty(Path))
    {
      return ValidationResult.Success();
    }

    if (!System.IO.Path.IsPathFullyQualified(Path))
    {
      return ValidationResult.Error("Path must be an absolute path.");
    }

    if (!Path.EndsWith("README.md", StringComparison.OrdinalIgnoreCase))
    {
      return ValidationResult.Error("Path must point to a README.md file.");
    }

    return ValidationResult.Success();
  }
}
