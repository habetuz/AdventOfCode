using Spectre.Console;

namespace AdventOfCode.Time
{
  public static class DateConverter
  {
    private const int START_YEAR = 2015;
    private const int START_DAY = 01;
    private const int END_DAY = 25;

    public static Date SingleDate(string date)
    {
      SingleDatePart(date, out Date d);
      return d;
    }

    public static CalendarRange DateRange(string dateRange)
    {
      DateRange(dateRange, out CalendarRange r);
      return r;
    }

    /**
    * Returns a single date but does not fill in missing information.
    */
    public static ValidationResult SingleDatePart(string stringDate, out Date date)
    {
      date = new Date();

      string[] dateParts = stringDate.Split(".");

      if (stringDate.Length == 0)
      {
        date.Year = -1;
        date.Day = -1;

        return ValidationResult.Success();
      }
      else if (dateParts.Length == 1)
      {
        stringDate = dateParts[0];

        if (!int.TryParse(stringDate, out int number))
        {
          return ValidationResult.Error($"{stringDate} is not a date.");
        }

        if (stringDate.Length <= 2)
        {
          date.Year = -1;
          date.Day = number;
        }
        else if (stringDate.Length == 4)
        {
          date.Year = number;
          date.Day = -1;
        }
        else
        {
          return ValidationResult.Error($"{stringDate} is not a date.");
        }
      }
      else if (dateParts.Length == 2)
      {
        string yearString = dateParts[0];
        string dayString = dateParts[1];
        if (!int.TryParse(yearString, out int year) || !int.TryParse(dayString, out int day))
        {
          return ValidationResult.Error($"{stringDate} is not a date.");
        }

        date.Year = yearString.Length <= 2 ? year + 2000 : year;
        date.Day = day;
      }
      else
      {
        return ValidationResult.Error($"{stringDate} is not a date.");
      }

      return ValidationResult.Success();
    }

    public static ValidationResult SingleDateFull(string stringDate, out Date date)
    {
      var result = SingleDatePart(stringDate!, out date);
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

      return ValidationResult.Success();
    }

    public static ValidationResult DateRange(string stringRange, out CalendarRange dateRange)
    {
      Date startDate = new() { Year = START_YEAR, Day = START_DAY };
      Date endDate;
      dateRange = new();

      if (string.IsNullOrEmpty(stringRange))
      {
        endDate = new()
        {
          Year = AOCDateTimeUtils.GetCurrentYear(),
          Day = AOCDateTimeUtils.GetCurrentDay(),
        };
        dateRange = new() { StartDate = startDate, EndDate = endDate };
        return ValidationResult.Success();
      }

      string[] dateParts = stringRange.Split("...");

      if (dateParts.Length == 1)
      {
        ValidationResult result = SingleDatePart(dateParts[0], out Date date);
        if (!result.Successful)
        {
          return result;
        }

        startDate = new()
        {
          Year = date.Year == -1 ? AOCDateTimeUtils.GetCurrentYear() : date.Year,
          Day = date.Day == -1 ? 01 : date.Day,
        };

        endDate = new()
        {
          Year = date.Year == -1 ? AOCDateTimeUtils.GetCurrentYear() : date.Year,
          Day =
            date.Day == -1
              ? date.Year == -1 || date.Year == AOCDateTimeUtils.GetCurrentYear()
                ? AOCDateTimeUtils.GetCurrentDay()
                : 25
              : date.Day,
        };
      }
      else if (dateParts.Length == 2)
      {
        ValidationResult result = SingleDatePart(dateParts[0], out startDate);
        if (!result.Successful)
        {
          return result;
        }

        result = SingleDatePart(dateParts[1], out endDate);
        if (!result.Successful)
        {
          return result;
        }

        startDate = new()
        {
          Year = startDate.Year == -1 ? AOCDateTimeUtils.GetCurrentYear() : startDate.Year,
          Day = startDate.Day == -1 ? 01 : startDate.Day,
        };

        endDate = new()
        {
          Year = endDate.Year == -1 ? AOCDateTimeUtils.GetCurrentYear() : endDate.Year,
          Day = endDate.Day == -1 ? 25 : endDate.Day,
        };
      }
      else
      {
        return ValidationResult.Error("[range] is invalid.");
      }

      if (startDate > endDate)
      {
        return ValidationResult.Error("[range] is invalid. Start date has to be before end date.");
      }
      else if (startDate.Year < START_YEAR || startDate.Year > AOCDateTimeUtils.GetCurrentYear())
      {
        return ValidationResult.Error(
          string.Format(
            "[range] is invalid. Start year has to be between 2015 and {0}.",
            AOCDateTimeUtils.GetCurrentYear()
          )
        );
      }
      else if (
        startDate.Year == AOCDateTimeUtils.GetCurrentYear()
        && startDate.Day > AOCDateTimeUtils.GetCurrentDay()
      )
      {
        return ValidationResult.Error(
          string.Format("[range] is invalid. Start day has to be before the current day.")
        );
      }
      else if (endDate.Day < START_DAY || endDate.Day > END_DAY)
      {
        return ValidationResult.Error(
          string.Format("[range] is invalid. End day has to be between 01 and 25.")
        );
      }
      else if (endDate.Year < START_YEAR || endDate.Year > AOCDateTimeUtils.GetCurrentYear())
      {
        return ValidationResult.Error(
          string.Format(
            "[range] is invalid. End year has to be between 2015 and {0}.",
            AOCDateTimeUtils.GetCurrentYear()
          )
        );
      }
      else if (
        endDate.Year == AOCDateTimeUtils.GetCurrentYear()
        && endDate.Day > AOCDateTimeUtils.GetCurrentDay()
      )
      {
        return ValidationResult.Error(
          string.Format("[range] is invalid. End day has to be before the current day.")
        );
      }
      else if (endDate.Day < START_DAY || endDate.Day > END_DAY)
      {
        return ValidationResult.Error(
          string.Format("[range] is invalid. End day has to be between 01 and 25.")
        );
      }

      dateRange = new() { StartDate = startDate, EndDate = endDate };

      return ValidationResult.Success();
    }
  }
}
