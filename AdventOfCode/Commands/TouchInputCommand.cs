using System.Diagnostics.CodeAnalysis;
using Spectre.Console.Cli;
using AdventOfCode.Commands.Settings;

namespace AdventOfCode.Commands
{
    public class TouchInputCommand : Command<TouchInputSettings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] TouchInputSettings settings)
        {
            new InputManager(
                new WebResourceManager())
                .TouchInput(
                    settings.Date,
                    settings.Example);
            return 0;
        }
    }
}