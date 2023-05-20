using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class SettingsValidationTests
    {
        [TestMethod]
        public void NothingSpecified()
        {
            Settings settings = new();
            Assert.IsTrue(settings.Validate().Successful);
            Assert.AreEqual(Settings.START_YEAR, settings.StartYear);
            Assert.AreEqual(Settings.END_YEAR, settings.EndYear);
            Assert.AreEqual(Settings.START_DAY, settings.StartDay);
            Assert.AreEqual(Settings.END_DAY, settings.EndDay);
        }

        [TestMethod]
        public void SingleNumberSpecified()
        {
            Settings settings = new()
            {
                Days = "25",
            };
            Assert.IsTrue(settings.Validate().Successful);
            Assert.AreEqual(Settings.START_YEAR, settings.StartYear);
            Assert.AreEqual(Settings.END_YEAR, settings.EndYear);
            Assert.AreEqual<uint>(25, settings.StartDay);
            Assert.AreEqual<uint>(25, settings.EndDay);

            settings = new()
            {
                Days = "10",
            };
            Assert.IsTrue(settings.Validate().Successful);
            Assert.AreEqual(Settings.START_YEAR, settings.StartYear);
            Assert.AreEqual(Settings.END_YEAR, settings.EndYear);
            Assert.AreEqual<uint>(10, settings.StartDay);
            Assert.AreEqual<uint>(10, settings.EndDay);

            settings = new()
            {
                Years = "2021",
            };
            Assert.IsTrue(settings.Validate().Successful);
            Assert.AreEqual<uint>(2021, settings.StartYear);
            Assert.AreEqual<uint>(2021, settings.EndYear);
            Assert.AreEqual(Settings.START_DAY, settings.StartDay);
            Assert.AreEqual(Settings.END_DAY, settings.EndDay);

            settings = new()
            {
                Years = "2022",
            };
            Assert.IsTrue(settings.Validate().Successful);
            Assert.AreEqual<uint>(2022, settings.StartYear);
            Assert.AreEqual<uint>(2022, settings.EndYear);
            Assert.AreEqual(Settings.START_DAY, settings.StartDay);
            Assert.AreEqual(Settings.END_DAY, settings.EndDay);

            settings = new()
            {
                Years = "2022",
                Days = "5"
            };
            Assert.IsTrue(settings.Validate().Successful);
            Assert.AreEqual<uint>(2022, settings.StartYear);
            Assert.AreEqual<uint>(2022, settings.EndYear);
            Assert.AreEqual<uint>(5, settings.StartDay);
            Assert.AreEqual<uint>(5, settings.EndDay);

            settings = new()
            {
                Days = (Settings.START_DAY - 1).ToString()
            };
            Assert.IsFalse(settings.Validate().Successful);

            settings = new()
            {
                Days = (Settings.END_DAY + 1).ToString()
            };
            Assert.IsFalse(settings.Validate().Successful);

            settings = new()
            {
                Years = (Settings.START_YEAR - 1).ToString(),
            };
            Assert.IsFalse(settings.Validate().Successful);

            settings = new()
            {
                Years = (Settings.END_YEAR + 1).ToString(),
            };
            Assert.IsFalse(settings.Validate().Successful);
        }

        [TestMethod]
        public void FullRangeSpecified()
        {
            Settings settings = new()
            {
                Years = "2021..2022",
                Days = "05..23",
            };
            Assert.IsTrue(settings.Validate().Successful);
            Assert.AreEqual<uint>(2021, settings.StartYear);
            Assert.AreEqual<uint>(2022, settings.EndYear);
            Assert.AreEqual<uint>(5, settings.StartDay);
            Assert.AreEqual<uint>(23, settings.EndDay);

            settings = new()
            {
                Years = "2022..2022",
                Days = "07..10",
            };
            Assert.IsTrue(settings.Validate().Successful);
            Assert.AreEqual<uint>(2022, settings.StartYear);
            Assert.AreEqual<uint>(2022, settings.EndYear);
            Assert.AreEqual<uint>(7, settings.StartDay);
            Assert.AreEqual<uint>(10, settings.EndDay);
        }

        [TestMethod]
        public void HalfRangeSpecified()
        {
            Settings settings = new()
            {
                Years = "..2022",
                Days = "..23",
            };
            Assert.IsTrue(settings.Validate().Successful);
            Assert.AreEqual<uint>(2021, settings.StartYear);
            Assert.AreEqual<uint>(2022, settings.EndYear);
            Assert.AreEqual<uint>(1, settings.StartDay);
            Assert.AreEqual<uint>(23, settings.EndDay);

            settings = new()
            {
                Years = "2021..",
                Days = "07..",
            };
            Assert.IsTrue(settings.Validate().Successful);
            Assert.AreEqual<uint>(2021, settings.StartYear);
            Assert.AreEqual<uint>(2022, settings.EndYear);
            Assert.AreEqual<uint>(7, settings.StartDay);
            Assert.AreEqual<uint>(25, settings.EndDay);
        }
    }
}