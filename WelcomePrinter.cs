namespace AdventOfCode
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Net.Http;
    using AngleSharp;
    using AngleSharp.Dom;
    using SharpLog;
    using Spectre.Console;

    internal static class WelcomePrinter
    {
        public static void Print(int year, HttpClient client)
        {
            try
            {
                // Start to download html (TODO: Check if html has changed)
                var html = client.GetStringAsync($"{year}");

                // Create parser
                var config = Configuration.Default.WithCss();
                var context = BrowsingContext.New(config);

                // Parse html
                var document = context.OpenAsync(req => req.Content(html.Result)).Result;

                // Get username and star count
                var userStarElement = document.GetElementsByClassName("user");
                if (userStarElement.Length == 0)
                {
                    Logging.LogFatal("The provided session cookie is not valide! Could not log in.", "RUNNER");
                }

                var userStar = document.GetElementsByClassName("user")[0].TextContent.Split(' ');
                var user = userStar[0];
                var star = userStar.Length > 1 ? userStar[1] : "0";

                var rule = new Rule($"[green]Logged in as [white]{user}[/] [yellow]{star}[/][/]")
                {
                    Style = Style.Parse("green"),
                    Border = BoxBorder.Double,
                };
                AnsiConsole.Write(new Padder(rule).Padding(0, 0, 0, 1));
                AnsiConsole.Write(new Rule()
                {
                    Style = Style.Parse("#0f0f23 on #0f0f23"),
                });

                foreach (var line in document.GetElementsByClassName("calendar")[0].Children)
                {
                    var input = line.TextContent;
                    var output = string.Empty;

                    foreach (var child in line.Children)
                    {
                        if (child.ClassName == "calendar-day")
                        {
                            output += $"[#666666]{Markup.Escape(input.Split(new string[] { child.TextContent }, StringSplitOptions.None)[0])}[/][white]{child.TextContent}[/] ";
                            break;
                        }

                        var color = HexConverter(ParseColor(document.DefaultView.GetComputedStyle(child).GetProperty("color").Value));
                        var reference = child.TextContent;

                        for (int i = 0; ; i++)
                        {
                            var comparison = input.Substring(i, reference.Length);
                            if (comparison == reference)
                            {
                                output += Markup.Escape(input.Substring(0, i));
                                output += $"[{color}]{Markup.Escape(reference)}[/]";
                                input = input.Substring(i + reference.Length);
                                break;
                            }
                        }
                    }

                    if (line.ClassList.Contains("calendar-complete"))
                    {
                        output += "[#ffff66]* [/]";
                    }
                    else if (line.ClassList.Contains("calendar-verycomplete"))
                    {
                        output += "[#ffff66]**[/]";
                    }
                    else
                    {
                        output += "  ";
                    }

                    var formattedLine = new Rule($"[on #0f0f23]{output}[/]")
                    {
                        Style = Style.Parse("#0f0f23 on #0f0f23"),
                    };
                    formattedLine.Centered();
                    AnsiConsole.Write(formattedLine);
                }

                AnsiConsole.Write(new Rule()
                {
                    Style = Style.Parse("on #0f0f23"),
                    Border = BoxBorder.None,
                });

                AnsiConsole.Write(new Padder(rule).Padding(0, 1, 0, 1));
            }
            catch (AggregateException)
            {
                Logging.LogFatal("Server request failed! Possible reasons might be:\n- The provided session cookie is outdated or wrong\n- The provided year is not available\n- You do not have an internet connection\n- The advent of code server is offline", "RUNNER");
            }
        }

        // From https://stackoverflow.com/a/40611610/11881711
        private static System.Drawing.Color ParseColor(string cssColor)
        {
            cssColor = cssColor.Trim();

            if (cssColor.StartsWith("#"))
            {
                return ColorTranslator.FromHtml(cssColor);
            }
            else if (cssColor.StartsWith("rgb"))
            {
                int left = cssColor.IndexOf('(');
                int right = cssColor.IndexOf(')');

                if (left < 0 || right < 0)
                {
                    throw new FormatException("rgba format error");
                }

                string noBrackets = cssColor.Substring(left + 1, right - left - 1);

                string[] parts = noBrackets.Split(',');

                int r = int.Parse(parts[0], CultureInfo.InvariantCulture);
                int g = int.Parse(parts[1], CultureInfo.InvariantCulture);
                int b = int.Parse(parts[2], CultureInfo.InvariantCulture);

                if (parts.Length == 3)
                {
                    return System.Drawing.Color.FromArgb(r, g, b);
                }
                else if (parts.Length == 4)
                {
                    float a = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    return System.Drawing.Color.FromArgb((int)(a * 255), r, g, b);
                }
            }

            throw new FormatException("Not rgb, rgba or hexa color string");
        }

        // From https://stackoverflow.com/a/2395708/11881711
        private static string HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
    }
}
