using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Commands.Settings;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands;

public class TouchInputCommand : Command<TouchInputSettings>
{
  public override int Execute(
    [NotNull] CommandContext context,
    [NotNull] TouchInputSettings settings
  )
  {
    new InputManager(new WebResourceManager()).TouchInput(settings.Date, settings.Example);
    return 0;
  }
}
