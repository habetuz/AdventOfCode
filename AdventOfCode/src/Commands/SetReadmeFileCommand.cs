using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Commands.Settings;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands;

public class SetReadmeFileCommand : Command<SetReadmeFileSettings>
{
  public override int Execute(
    [NotNull] CommandContext context,
    [NotNull] SetReadmeFileSettings settings
  )
  {
    ApplicationSettings.Instance.ReadmePath = settings.Path;

    return 0;
  }
}
