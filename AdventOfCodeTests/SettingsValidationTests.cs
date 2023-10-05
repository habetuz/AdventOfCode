using Spectre.Console;

namespace AdventOfCodeTests
{
    [TestClass]
    public class SettingsValidationTests
    {
        [TestMethod]
        public void Validate_YearOnly()
        {
            DateTime now = DateTime.Now;

            RunSettings settings = new()
            {
                Range = "2022",
            };
            ValidationResult result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(01, settings.StartDate.Day);
            Assert.AreEqual(25, settings.EndDate.Day);
            Assert.AreEqual(2022, settings.StartDate.Year);
            Assert.AreEqual(2022, settings.EndDate.Year);

            settings = new()
            {
                Range = "2014",
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);

            settings = new()
            {
                Range = (AOCDateTimeUtils.GetCurrentYear(ref now) + 1).ToString(),
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);
        }

        [TestMethod]
        public void Validate_DayOnly()
        {
            DateTime now = DateTime.Now;

            RunSettings settings = new()
            {
                Range = "5",
            };
            ValidationResult result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(05, settings.StartDate.Day);
            Assert.AreEqual(05, settings.EndDate.Day);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.StartDate.Year);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.EndDate.Year);

            settings = new()
            {
                Range = "20",
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(20, settings.StartDate.Day);
            Assert.AreEqual(20, settings.EndDate.Day);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.StartDate.Year);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.EndDate.Year);

            settings = new()
            {
                Range = "0",
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);

            settings = new()
            {
                Range = (AOCDateTimeUtils.GetCurrentDay(ref now) + 1).ToString(),
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);
        }

        [TestMethod]
        public void Validate_FullDate()
        {
            DateTime now = DateTime.Now;

            RunSettings settings = new()
            {
                Range = "2017.5",
            };
            ValidationResult result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(05, settings.StartDate.Day);
            Assert.AreEqual(05, settings.EndDate.Day);
            Assert.AreEqual(2017, settings.StartDate.Year);
            Assert.AreEqual(2017, settings.EndDate.Year);

            settings = new()
            {
                Range = "18.24",
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(24, settings.StartDate.Day);
            Assert.AreEqual(24, settings.EndDate.Day);
            Assert.AreEqual(2018, settings.StartDate.Year);
            Assert.AreEqual(2018, settings.EndDate.Year);

            settings = new()
            {
                Range = "13.05",
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);

            settings = new()
            {
                Range = (AOCDateTimeUtils.GetCurrentDay(ref now) + 1).ToString() + ".13",
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);

            settings = new()
            {
                Range = "2017.27",
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);
        }

        [TestMethod]
        public void Validate_HalfRange()
        {
            DateTime now = DateTime.Now;

            RunSettings settings = new()
            {
                Range = "5...",
            };
            ValidationResult result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(05, settings.StartDate.Day);
            Assert.AreEqual(25, settings.EndDate.Day);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.StartDate.Year);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.EndDate.Year);

            settings = new()
            {
                Range = "...20",
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(01, settings.StartDate.Day);
            Assert.AreEqual(20, settings.EndDate.Day);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.StartDate.Year);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.EndDate.Year);

            settings = new()
            {
                Range = "..." + AOCDateTimeUtils.GetCurrentYear(ref now),
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(01, settings.StartDate.Day);
            Assert.AreEqual(25, settings.EndDate.Day);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.StartDate.Year);
            Assert.AreEqual(2022, settings.EndDate.Year);

            settings = new()
            {
                Range = "2022...",
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(01, settings.StartDate.Day);
            Assert.AreEqual(25, settings.EndDate.Day);
            Assert.AreEqual(2022, settings.StartDate.Year);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.EndDate.Year);

            settings = new()
            {
                Range = "20.10...",
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(10, settings.StartDate.Day);
            Assert.AreEqual(25, settings.EndDate.Day);
            Assert.AreEqual(2020, settings.StartDate.Year);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.EndDate.Year);

            settings = new()
            {
                Range = "..." + AOCDateTimeUtils.GetCurrentYear(ref now) + ".15",
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(01, settings.StartDate.Day);
            Assert.AreEqual(15, settings.EndDate.Day);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.StartDate.Year);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.EndDate.Year);
        }

        [TestMethod]
        public void Validate_FullRange()
        {
            DateTime now = DateTime.Now;

            RunSettings settings = new()
            {
                Range = "5...08",
            };
            ValidationResult result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(05, settings.StartDate.Day);
            Assert.AreEqual(08, settings.EndDate.Day);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.StartDate.Year);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.EndDate.Year);

            settings = new()
            {
                Range = "2017...20",
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(01, settings.StartDate.Day);
            Assert.AreEqual(20, settings.EndDate.Day);
            Assert.AreEqual(2017, settings.StartDate.Year);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.EndDate.Year);

            settings = new()
            {
                Range = "19.23...2022",
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(23, settings.StartDate.Day);
            Assert.AreEqual(25, settings.EndDate.Day);
            Assert.AreEqual(2019, settings.StartDate.Year);
            Assert.AreEqual(2022, settings.EndDate.Year);

            settings = new()
            {
                Range = "2022...2022.18",
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(01, settings.StartDate.Day);
            Assert.AreEqual(18, settings.EndDate.Day);
            Assert.AreEqual(2022, settings.StartDate.Year);
            Assert.AreEqual(2022, settings.EndDate.Year);

            settings = new()
            {
                Range = "20.10...21.10",
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(10, settings.StartDate.Day);
            Assert.AreEqual(10, settings.EndDate.Day);
            Assert.AreEqual(2020, settings.StartDate.Year);
            Assert.AreEqual(2021, settings.EndDate.Year);

            settings = new()
            {
                Range = "15.22...2022.15",
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(22, settings.StartDate.Day);
            Assert.AreEqual(15, settings.EndDate.Day);
            Assert.AreEqual(2015, settings.StartDate.Year);
            Assert.AreEqual(2022, settings.EndDate.Year);

            settings = new()
            {
                Range = "...",
            };
            result = settings.Validate();
            Assert.IsTrue(result.Successful);
            Assert.AreEqual(01, settings.StartDate.Day);
            Assert.AreEqual(25, settings.EndDate.Day);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.StartDate.Year);
            Assert.AreEqual(AOCDateTimeUtils.GetCurrentYear(ref now), settings.EndDate.Year);
        }

        [TestMethod]
        public void Validate_FlippedDates()
        {
            RunSettings settings = new()
            {
                Range = "2017.27...2017.20",
            };
            ValidationResult result = settings.Validate();
            Assert.IsFalse(result.Successful);

            settings = new()
            {
                Range = "10...2015.12",
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);

            settings = new()
            {
                Range = "2018...2015",
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);

            settings = new()
            {
                Range = "10...5",
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);
        }

        [TestMethod]
        public void Validate_WrongDigits()
        {
            RunSettings settings = new()
            {
                Range = "017.27...2017.20",
            };
            ValidationResult result = settings.Validate();
            Assert.IsFalse(result.Successful);

            settings = new()
            {
                Range = "100",
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);

            settings = new()
            {
                Range = "20185...2015",
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);

            settings = new()
            {
                Range = "20151",
            };
            result = settings.Validate();
            Assert.IsFalse(result.Successful);
        }
    }
}