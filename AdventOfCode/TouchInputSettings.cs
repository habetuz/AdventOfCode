using System.ComponentModel;
using Spectre.Console.Cli;

namespace AdventOfCode
{
    public class TouchInputSettings : CommandSettings
    {
        [Description("The date of the input you want to create/open.")]
        [CommandArgument(0, "<date>")]
        public string StringDate { get; init; }
        public uint? Example { get; init; }
    }
}