using System.Collections;

namespace AdventOfCode
{
    public partial class CalendarRange : IEnumerable<Date>
    {
        private Date startDate;
        private Date endDate;

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
                this.current = this.startDate;
            }

            public Date Current => this.current;
            object IEnumerator.Current => this.current;

            public void Dispose() => GC.SuppressFinalize(this);

            public bool MoveNext()
            {
                this.current.Day++;
                if (this.current.Day > 25)
                {
                    this.current.Day = 1;
                    this.current.Year++;
                }

                return this.current <= this.endDate;
            }

            public void Reset()
            {
                this.current = this.startDate;
            }
        }
    }
}