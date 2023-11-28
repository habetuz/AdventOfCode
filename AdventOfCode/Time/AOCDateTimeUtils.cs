namespace AdventOfCode.Time
{
    public static class AOCDateTimeUtils
    {
        public static int GetCurrentYear(DateTime? currentDate = null)
        {
            if (!currentDate.HasValue)
            {
                currentDate = DateTime.Now;
            }

            int currentYear = currentDate.Value.Year;

            if (currentDate.Value.Month < 12)
            {
                return (int)(currentYear - 1);
            }
            else
            {
                return (int)currentYear;
            }
        }

        public static int GetCurrentDay(DateTime? currentDate = null)
        {
            if (!currentDate.HasValue)
            {
                currentDate = DateTime.Now;
            }

            int currentDay = currentDate.Value.Day;

            if (currentDate.Value.Month == 12 && currentDay >= 1 && currentDay <= 25)
            {
                return (int)currentDay;
            }

            return 25;
        }
    }
}
