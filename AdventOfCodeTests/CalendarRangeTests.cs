namespace AdventOfCodeTests
{
    [TestClass]
    public class CalendarRangeTests
    {
        [TestMethod]
        public void Enumerator()
        {
            CalendarRange calendarRange = new()
            {
                StartDay = 1,
                StartYear = 2015,
                EndDay = 5,
                EndYear = 2015,
            };

            List<CalendarRange.Date> expectedDates = new()
            {
                new CalendarRange.Date() { Year = 2015, Day = 01 },
                new CalendarRange.Date() { Year = 2015, Day = 02 },
                new CalendarRange.Date() { Year = 2015, Day = 03 },
                new CalendarRange.Date() { Year = 2015, Day = 04 },
                new CalendarRange.Date() { Year = 2015, Day = 05 },
            };

            List<CalendarRange.Date> actualDates = new();

            actualDates.AddRange(calendarRange);

            CollectionAssert.AreEqual(expectedDates, actualDates);

            calendarRange = new()
            {
                StartDay = 25,
                StartYear = 2015,
                EndDay = 5,
                EndYear = 2016,
            };

            expectedDates = new()
            {
                new CalendarRange.Date() { Year = 2015, Day = 25 },
                new CalendarRange.Date() { Year = 2016, Day = 01 },
                new CalendarRange.Date() { Year = 2016, Day = 02 },
                new CalendarRange.Date() { Year = 2016, Day = 03 },
                new CalendarRange.Date() { Year = 2016, Day = 04 },
                new CalendarRange.Date() { Year = 2016, Day = 05 },
            };

            actualDates = new();

            actualDates.AddRange(calendarRange);

            CollectionAssert.AreEqual(expectedDates, actualDates);

            calendarRange = new()
            {
                StartDay = 25,
                StartYear = 2015,
                EndDay = 5,
                EndYear = 2014,
            };

            expectedDates = new();

            actualDates = new();

            actualDates.AddRange(calendarRange);

            CollectionAssert.AreEqual(expectedDates, actualDates);
        }

        [TestMethod]
        public void Date_OperatorEquals()
        {
            CalendarRange.Date a = new()
            {
                Year = 2022,
                Day = 05,
            };

            CalendarRange.Date b = a;
            Assert.AreEqual(a, b);
            Assert.IsTrue(a == b);

            b.Year = 2021;
            Assert.IsFalse(a == b);
            Assert.AreNotEqual(a, b);

            b = a;
            b.Day = 07;
            Assert.IsFalse(a == b);
            Assert.AreNotEqual(a, b);
        }

        [TestMethod]
        public void Date_OperatorLessThan()
        {
            CalendarRange.Date a = new()
            {
                Year = 2022,
                Day = 05,
            };

            CalendarRange.Date b = a;
            b.Day++;
            Assert.IsTrue(a < b);
            Assert.IsFalse(b < a);

            a.Year++;
            Assert.IsTrue(b < a);
            Assert.IsFalse(a < b);
        }

        [TestMethod]
        public void Date_OperatorMoreThan()
        {
            CalendarRange.Date a = new()
            {
                Year = 2022,
                Day = 05,
            };

            CalendarRange.Date b = a;
            b.Day++;
            Assert.IsFalse(a > b);
            Assert.IsTrue(b > a);

            a.Year++;
            Assert.IsFalse(b > a);
            Assert.IsTrue(a > b);
        }
    }
}