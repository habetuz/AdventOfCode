using System.ComponentModel;
using AdventOfCode.Time;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands.Settings
{
    public class SubmitSettings : CommandSettings
    {
        [Description(
            "The date of the input you want to submit as solution."
        )]
        [CommandArgument(0, "<date>")]
        public string? StringDate { get; init; } = "";

        [Description(
            "The solution for puzzle one."
        )]
        [CommandArgument(1, "[solution 1]")]
        public string? Solution1 { get; init; } = "";

        [Description(
            "The solution for puzzle two."
        )]
        [CommandArgument(1, "[solution 1]")]
        public string? Solution2 { get; init; } = "";

        public Date Date { get; private set; }

        public override ValidationResult Validate()
        {
            Date date;
            var result = DateConverter.SingleDate(this.StringDate!, out date);
            if (!result.Successful)
            {
                return result;
            }

            var currentTime = DateTime.Now;

            date.Year = date.Year == -1 ? AOCDateTimeUtils.GetCurrentYear(currentTime) : date.Year;
            date.Day = date.Day == -1 ? AOCDateTimeUtils.GetCurrentDay(currentTime) : date.Day;

            if (
                date.Year > AOCDateTimeUtils.GetCurrentYear(currentTime)
                || date.Year < 2015
                || date.Day
                    > (
                        currentTime.Year == AOCDateTimeUtils.GetCurrentYear(currentTime)
                            ? AOCDateTimeUtils.GetCurrentDay(currentTime)
                            : 25
                    )
                || date.Day < 1
            )
            {
                return ValidationResult.Error("The provided date is out of range.");
            }

            this.Date = date;

            if (string.IsNullOrEmpty(Solution1) && string.IsNullOrEmpty(Solution2))
            {
                return ValidationResult.Error("No solution provided.");
            }
            
            return ValidationResult.Success();
        }
    }
}
