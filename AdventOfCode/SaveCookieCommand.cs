using System.Diagnostics.CodeAnalysis;
using Config.Net;
using SharpLog;
using Spectre.Console.Cli;

namespace AdventOfCode
{
    public class SaveCookieCommand : Command<SaveCookieSettings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] SaveCookieSettings settings)
        {
            ApplicationSettings.Instance.Cookie = settings.Cookie;

            return 0;
        }
    }
}