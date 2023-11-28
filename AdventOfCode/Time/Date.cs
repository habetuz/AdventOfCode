namespace AdventOfCode.Time
{
    public struct Date
    {
        public static Date First
        {
            get => new Date(2015, 01);
        }

        public static Date Last
        {
            get => new Date(AOCDateTimeUtils.GetCurrentYear(), AOCDateTimeUtils.GetCurrentDay());
        }

        public int Year { get; set; }
        public int Day { get; set; }

        public Date() { }

        public Date(int year, int day)
        {
            this.Year = year;
            this.Day = day;
        }

        public override readonly bool Equals(object? obj)
        {
            if (obj is not Date other)
            {
                return false;
            }

            return this == other;
        }

        public static bool operator ==(Date a, Date b) => a.Year == b.Year && a.Day == b.Day;

        public static bool operator !=(Date a, Date b) => !(a == b);

        public static bool operator <(Date a, Date b) =>
            a.Year <= b.Year && (a.Year < b.Year || a.Day < b.Day);

        public static bool operator >(Date a, Date b) => a != b && !(a < b);

        public static bool operator <=(Date a, Date b) => a < b || a == b;

        public static bool operator >=(Date a, Date b) => a > b || a == b;

        public override readonly int GetHashCode()
        {
            return (int)(this.Year + (this.Day * 100));
        }

        public override readonly string ToString()
        {
            return string.Format("{0:D2}.{1:D2}", this.Year, this.Day);
        }
    }
}
