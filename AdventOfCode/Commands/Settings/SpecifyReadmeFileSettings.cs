using System;
using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands.Settings
{
    public class SpecifyReadmeFileSettings : CommandSettings
    {
        [Description("The path to your README.md file. It will be updated as you solve puzzles.")]
        [CommandArgument(0, "<cookie>")]
        public string Path { get; init; } = null!;

        public override ValidationResult Validate()
        {
            return ValidationResult.Success();
            // TODO: Validate path
        }
    }
}
