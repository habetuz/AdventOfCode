using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode
{
    public class Settings : CommandSettings
    {
        public const uint START_YEAR = 2021;
        public const uint END_YEAR = 2022;
        public const uint START_DAY = 01;
        public const uint END_DAY = 25;

        [Description("The session cookie. Copy it from your browser.")]
        [CommandArgument(0, "<cookie>")]
        public string Cookie { get; init; } = null!;

        [Description("The year (eg. '2022') or year range (eg. '..2020' '2020..' '2020..2021'). Provide no value for all years.")]
        [CommandOption("-y|--years")]

        public string? Years { get; init; }

        [Description("The day (eg. '01') or day range (eg. '..06' '01..' '01..10'). Provide no value for all days.")]
        [CommandOption("-d|--days")]
        public string? Days { get; init; }

        public uint StartYear { get; private set; } = START_YEAR;
        public uint EndYear { get; private set; } = END_YEAR;
        public uint StartDay { get; private set; } = START_DAY;
        public uint EndDay { get; private set; } = END_DAY;

        public override ValidationResult Validate()
        {
            if (!string.IsNullOrEmpty(this.Years))
            {
                string[] yearRange = this.Years.Split("..");
                if (yearRange.Length == 1)
                {
                    if (!uint.TryParse(this.Years, out uint year))
                    {
                        return ValidationResult.Error("Option [years]|-y|--years is not valide. Check -h for more format information.");
                    }

                    this.StartYear = year;
                    this.EndYear = year;
                }
                else if (yearRange.Length == 2)
                {
                    if (yearRange[0].Length > 0)
                    {
                        if (!uint.TryParse(yearRange[0], out uint startYear))
                        {
                            return ValidationResult.Error("Option [years]|-y|--years is not valide. The start year is not a number. Check -h for more format information.");
                        }

                        this.StartYear = startYear;
                    }

                    if (yearRange[1].Length > 0)
                    {
                        if (!uint.TryParse(yearRange[1], out uint endYear))
                        {
                            return ValidationResult.Error("Option [years]|-y|--years is not valide. The end year is not a number. Check -h for more format information.");
                        }

                        this.EndYear = endYear;
                    }
                }
                else
                {
                    return ValidationResult.Error("Option [years]|-y|--years is not valide. Check -h for more format information.");
                }
            }

            if (!string.IsNullOrEmpty(this.Days))
            {
                string[] dayRange = this.Days.Split("..");
                if (dayRange.Length == 1)
                {
                    if (!uint.TryParse(this.Days, out uint day))
                    {
                        return ValidationResult.Error("Option [days]|-d|--days is not valide. Check -h for more format information.");
                    }

                    this.StartDay = day;
                    this.EndDay = day;
                }
                else if (dayRange.Length == 2)
                {
                    if (dayRange[0].Length > 0)
                    {
                        if (!uint.TryParse(dayRange[0], out uint startDay))
                        {
                            return ValidationResult.Error("Option [days]|-d|--days is not valide. The start day is not a number. Check -h for more format information.");
                        }

                        this.StartDay = startDay;
                    }

                    if (dayRange[1].Length > 0)
                    {
                        if (!uint.TryParse(dayRange[1], out uint endDay))
                        {
                            return ValidationResult.Error("Option [days]|-d|--days is not valide. The end day is not a number. Check -h for more format information.");
                        }

                        this.EndDay = endDay;
                    }
                }
                else
                {
                    return ValidationResult.Error("Option [days]|-d|--days is not valide. Check -h for more format information.");
                }
            }

            if (this.StartYear < START_YEAR)
            {
                return ValidationResult.Error(string.Format("Option [years]|-y|--years is not valide. The start year has to be at least {0}.", START_YEAR));
            }
            else if (this.EndYear > END_YEAR)
            {
                return ValidationResult.Error(string.Format("Option [years]|-y|--years is not valide. The end year has to be maximum {0}.", END_YEAR));
            }
            else if (this.StartDay < START_DAY)
            {
                return ValidationResult.Error(string.Format("Option [days]|-d|--days is not valide. The start day has to be at least {0}.", START_DAY));
            }
            else if (this.EndDay > END_DAY)
            {
                return ValidationResult.Error(string.Format("Option [days]|-d|--days is not valide. The end day has to be maximum {0}.", END_DAY));
            }

            return ValidationResult.Success();
        }
    }
}