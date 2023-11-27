
using System;
using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Commands.Settings;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands;

public class SpecifyReadmeFileCommand : Command<SpecifyReadmeFileSettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] SpecifyReadmeFileSettings settings)
    {
        ApplicationSettings.Instance.ReadmePath = settings.Path;

        return 0;
    }
}
