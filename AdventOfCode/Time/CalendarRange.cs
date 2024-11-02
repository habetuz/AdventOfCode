using System.Collections;
using YamlDotNet.Serialization.ObjectGraphTraversalStrategies;

namespace AdventOfCode.Time
{
    public partial class CalendarRange : IEnumerable<Date>
    {
        private Date startDate;
        private Date endDate;

        public static CalendarRange Full
        {
            get => new(Date.First, Date.Last);
        }

        public IEnumerator<Date> GetEnumerator()
        {
            return new CalendarRangeEnumerator(startDate, endDate);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Date StartDate
        {
            get => startDate;
            init => startDate = value;
        }

        public Date EndDate
        {
            get => endDate;
            init => endDate = value;
        }

        public int StartDay
        {
            get => startDate.Day;
            init => startDate.Day = value;
        }

        public int EndDay
        {
            get => endDate.Day;
            init => endDate.Day = value;
        }

        public int StartYear
        {
            get => startDate.Year;
            init => startDate.Year = value;
        }

        public int EndYear
        {
            get => endDate.Year;
            init => endDate.Year = value;
        }

        public CalendarRange() { }

        public CalendarRange(Date start, Date end)
        {
            startDate = start;
            endDate = end;
        }

        public class CalendarRangeEnumerator : IEnumerator<Date>
        {
            private Date startDate;
            private Date endDate;
            private Date current;

            public CalendarRangeEnumerator(Date startDate, Date endDate)
            {
                this.startDate = startDate;
                this.endDate = endDate;
                this.startDate.Day--;
                current = this.startDate;
            }

            public Date Current => current;
            object IEnumerator.Current => current;

            public void Dispose() => GC.SuppressFinalize(this);

            public bool MoveNext()
            {
                current.Day++;
                if (current.Day > 25)
                {
                    current.Day = 1;
                    current.Year++;
                }

                return current <= endDate;
            }

            public void Reset()
            {
                current = startDate;
            }
        }
    }
}
