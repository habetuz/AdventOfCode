namespace AdventOfCode
{
    using AngleSharp;
    using AngleSharp.Dom;
    using Pastel;
    using SharpLog;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Net.Http;

    internal static class WelcomePrinter
    {
        public static void Print(int year, int day, int test, HttpClient client)
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
                var star = userStar[1];

                Logging.LogInfo($"Logged in as {user.Pastel("#ffffff")} {star.Pastel("#ffff66")}", "RUNNER");

                for (int d = 1; d <= 25; d++)
                {
                    var line = document.GetElementsByClassName($"calendar-day{d}")[0];
                    var input = line.TextContent;
                    var output = string.Empty;

                    foreach (var child in line.Children)
                    {
                        if (child.ClassName == "calendar-day")
                        {
                            output += $"{input.Split(d.ToString()[0])[0].Pastel("#666666")}{d} ";
                            break;
                        }

                        var color = ParseColor(document.DefaultView.GetComputedStyle(child).GetProperty("color").Value);
                        var reference = child.TextContent;

                        for (int i = 0; ; i++)
                        {
                            var comparison = input.Substring(i, reference.Length);
                            if (comparison == reference)
                            {
                                output += input.Substring(0, i);
                                output += reference.Pastel(color);
                                input = input.Substring(i + reference.Length);
                                break;
                            }
                        }
                    }

                    if (line.ClassList.Contains("calendar-complete"))
                    {
                        output += "* ".Pastel("ffff66");
                    }
                    else if (line.ClassList.Contains("calendar-verycomplete"))
                    {
                        output += "**".Pastel("ffff66");
                    }
                    else
                    {
                        output += "  ";
                    }

                    Console.WriteLine(output.PastelBg("#0f0f23"));
                }
            }
            catch (AggregateException e)
            {
                Logging.LogFatal("Server request failed! Possible reasons might be:\n- The provided session cookie is outdated or wrong\n- The provided year is not available\n- You do not have an internet connection\n- The advent of code server is offline", "RUNNER");
            }
        }

        // From https://stackoverflow.com/a/40611610/11881711
        public static Color ParseColor(string cssColor)
        {
            cssColor = cssColor.Trim();

            if (cssColor.StartsWith("#"))
            {
                return ColorTranslator.FromHtml(cssColor);
            }
            else if (cssColor.StartsWith("rgb")) //rgb or argb
            {
                int left = cssColor.IndexOf('(');
                int right = cssColor.IndexOf(')');

                if (left < 0 || right < 0)
                    throw new FormatException("rgba format error");
                string noBrackets = cssColor.Substring(left + 1, right - left - 1);

                string[] parts = noBrackets.Split(',');

                int r = int.Parse(parts[0], CultureInfo.InvariantCulture);
                int g = int.Parse(parts[1], CultureInfo.InvariantCulture);
                int b = int.Parse(parts[2], CultureInfo.InvariantCulture);

                if (parts.Length == 3)
                {
                    return Color.FromArgb(r, g, b);
                }
                else if (parts.Length == 4)
                {
                    float a = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    return Color.FromArgb((int)(a * 255), r, g, b);
                }
            }
            throw new FormatException("Not rgb, rgba or hexa color string");
        }
    }
}
