namespace AdventOfCode.Time
{
    public static class AOCDateTimeUtils
    {
        public static int GetCurrentYear(DateTime currentDate)
        {
            int currentYear = currentDate.Year;

            if (currentDate.Month < 12)
            {
                return (int)(currentYear - 1);
            }
            else
            {
                return (int)currentYear;
            }
        }

        public static int GetCurrentDay(DateTime currentDate)
        {
            int currentDay = currentDate.Day;

            if (currentDate.Month == 12 && currentDay >= 1 && currentDay <= 25)
            {
                return (int)currentDay;
            }

            return 25;
        }
    }
}