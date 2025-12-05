using AdventOfCode.Time;
using Spectre.Console;

namespace AdventOfCodeTests;

[TestClass]
public class DateConverterTests
{
  [TestClass]
  public class SingleDatePartTests
  {
    [TestMethod]
    public void SingleDatePart_EmptyString_ReturnsInvalidDate()
    {
      var result = DateConverter.SingleDatePart("", out Date date);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(-1, date.Year);
      Assert.AreEqual(-1, date.Day);
    }

    [TestMethod]
    public void SingleDatePart_DayOnly_ParsesDay()
    {
      var result = DateConverter.SingleDatePart("15", out Date date);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(-1, date.Year);
      Assert.AreEqual(15, date.Day);
    }

    [TestMethod]
    public void SingleDatePart_TwoDigitDay_ParsesDay()
    {
      var result = DateConverter.SingleDatePart("05", out Date date);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(-1, date.Year);
      Assert.AreEqual(5, date.Day);
    }

    [TestMethod]
    public void SingleDatePart_FourDigitYear_ParsesYear()
    {
      var result = DateConverter.SingleDatePart("2023", out Date date);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(2023, date.Year);
      Assert.AreEqual(-1, date.Day);
    }

    [TestMethod]
    public void SingleDatePart_FullDateWithDot_ParsesYearAndDay()
    {
      var result = DateConverter.SingleDatePart("2023.15", out Date date);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(2023, date.Year);
      Assert.AreEqual(15, date.Day);
    }

    [TestMethod]
    public void SingleDatePart_TwoDigitYear_ConvertsTo2000s()
    {
      var result = DateConverter.SingleDatePart("23.15", out Date date);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(2023, date.Year);
      Assert.AreEqual(15, date.Day);
    }

    [TestMethod]
    public void SingleDatePart_InvalidFormat_ReturnsError()
    {
      var result = DateConverter.SingleDatePart("abc", out Date date);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void SingleDatePart_ThreeDigitNumber_ReturnsError()
    {
      var result = DateConverter.SingleDatePart("123", out Date date);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void SingleDatePart_TooManyParts_ReturnsError()
    {
      var result = DateConverter.SingleDatePart("2023.12.15", out Date date);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void SingleDatePart_NonNumericYear_ReturnsError()
    {
      var result = DateConverter.SingleDatePart("abc.15", out Date date);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void SingleDatePart_NonNumericDay_ReturnsError()
    {
      var result = DateConverter.SingleDatePart("2023.xyz", out Date date);

      Assert.IsFalse(result.Successful);
    }
  }

  [TestClass]
  public class SingleDateFullTests
  {
    [TestMethod]
    public void SingleDateFull_ValidDate_ReturnsSuccess()
    {
      var result = DateConverter.SingleDateFull("2023.10", out Date date);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(2023, date.Year);
      Assert.AreEqual(10, date.Day);
    }

    [TestMethod]
    public void SingleDateFull_YearOnly_FillsCurrentDay()
    {
      var result = DateConverter.SingleDateFull("2023", out Date date);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(2023, date.Year);
      Assert.IsTrue(date.Day >= 1 && date.Day <= 25);
    }

    [TestMethod]
    public void SingleDateFull_YearTooOld_ReturnsError()
    {
      var result = DateConverter.SingleDateFull("2014.10", out Date date);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void SingleDateFull_YearTooNew_ReturnsError()
    {
      var result = DateConverter.SingleDateFull("2030.10", out Date date);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void SingleDateFull_DayTooLow_ReturnsError()
    {
      var result = DateConverter.SingleDateFull("2023.0", out Date date);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void SingleDateFull_DayTooHigh_ReturnsError()
    {
      var result = DateConverter.SingleDateFull("2023.26", out Date date);

      Assert.IsFalse(result.Successful);
    }
  }

  [TestClass]
  public class DateRangeTests
  {
    [TestMethod]
    public void DateRange_EmptyString_ReturnsFullRange()
    {
      var result = DateConverter.DateRange("", out CalendarRange range);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(2015, range.StartYear);
      Assert.AreEqual(1, range.StartDay);
      Assert.IsGreaterThanOrEqualTo(2015, range.EndYear);
    }

    [TestMethod]
    public void DateRange_SingleDate_ReturnsSameDateRange()
    {
      var result = DateConverter.DateRange("2023.15", out CalendarRange range);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(2023, range.StartYear);
      Assert.AreEqual(15, range.StartDay);
      Assert.AreEqual(2023, range.EndYear);
      Assert.AreEqual(15, range.EndDay);
    }

    [TestMethod]
    public void DateRange_YearOnly_ReturnsFullYear()
    {
      var result = DateConverter.DateRange("2023", out CalendarRange range);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(2023, range.StartYear);
      Assert.AreEqual(1, range.StartDay);
      Assert.AreEqual(2023, range.EndYear);
      Assert.AreEqual(25, range.EndDay);
    }

    [TestMethod]
    public void DateRange_ValidRange_ParsesBothDates()
    {
      var result = DateConverter.DateRange("2023.10...2023.15", out CalendarRange range);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(2023, range.StartYear);
      Assert.AreEqual(10, range.StartDay);
      Assert.AreEqual(2023, range.EndYear);
      Assert.AreEqual(15, range.EndDay);
    }

    [TestMethod]
    public void DateRange_CrossYearRange_ParsesCorrectly()
    {
      var result = DateConverter.DateRange("2022.20...2023.10", out CalendarRange range);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(2022, range.StartYear);
      Assert.AreEqual(20, range.StartDay);
      Assert.AreEqual(2023, range.EndYear);
      Assert.AreEqual(10, range.EndDay);
    }

    [TestMethod]
    public void DateRange_StartYearOnly_FillsDay1()
    {
      var result = DateConverter.DateRange("2023...2024", out CalendarRange range);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(2023, range.StartYear);
      Assert.AreEqual(1, range.StartDay);
      Assert.AreEqual(2024, range.EndYear);
      Assert.AreEqual(25, range.EndDay);
    }

    [TestMethod]
    public void DateRange_StartAfterEnd_ReturnsError()
    {
      var result = DateConverter.DateRange("2023.20...2023.10", out CalendarRange range);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void DateRange_StartYearTooEarly_ReturnsError()
    {
      var result = DateConverter.DateRange("2014.10...2023.15", out CalendarRange range);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void DateRange_StartYearTooLate_ReturnsError()
    {
      var result = DateConverter.DateRange("2030.10...2031.15", out CalendarRange range);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void DateRange_EndYearTooEarly_ReturnsError()
    {
      var result = DateConverter.DateRange("2015.10...2014.15", out CalendarRange range);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void DateRange_EndYearTooLate_ReturnsError()
    {
      var result = DateConverter.DateRange("2023.10...2030.15", out CalendarRange range);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void DateRange_EndDayTooLowForOldYears_ReturnsError()
    {
      var result = DateConverter.DateRange("2023.10...2024.0", out CalendarRange range);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void DateRange_EndDayTooHighForOldYears_ReturnsError()
    {
      var result = DateConverter.DateRange("2023.10...2024.26", out CalendarRange range);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void DateRange_EndDayTooHighFor2025_ReturnsError()
    {
      var result = DateConverter.DateRange("2025.1...2025.13", out CalendarRange range);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void DateRange_ValidDay12For2025_ReturnsSuccess()
    {
      // Only run this test after 2025
      if (DateTime.Now.Year <= 2025)
      {
        return;
      }

      var result = DateConverter.DateRange("2025.1...2025.12", out CalendarRange range);

      Assert.IsTrue(result.Successful);
      Assert.AreEqual(2025, range.StartYear);
      Assert.AreEqual(1, range.StartDay);
      Assert.AreEqual(2025, range.EndYear);
      Assert.AreEqual(12, range.EndDay);
    }

    [TestMethod]
    public void DateRange_TooManyParts_ReturnsError()
    {
      var result = DateConverter.DateRange("2023.10...2024.15...2025.20", out CalendarRange range);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void DateRange_InvalidStartDate_ReturnsError()
    {
      var result = DateConverter.DateRange("abc...2024.15", out CalendarRange range);

      Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void DateRange_InvalidEndDate_ReturnsError()
    {
      var result = DateConverter.DateRange("2023.10...xyz", out CalendarRange range);

      Assert.IsFalse(result.Successful);
    }
  }

  [TestClass]
  public class SingleDateHelperTests
  {
    [TestMethod]
    public void SingleDate_ValidInput_ReturnsDate()
    {
      Date date = DateConverter.SingleDate("2023.15");

      Assert.AreEqual(2023, date.Year);
      Assert.AreEqual(15, date.Day);
    }

    [TestMethod]
    public void DateRange_ValidInput_ReturnsCalendarRange()
    {
      CalendarRange range = DateConverter.DateRange("2023.10...2023.15");

      Assert.AreEqual(2023, range.StartYear);
      Assert.AreEqual(10, range.StartDay);
      Assert.AreEqual(2023, range.EndYear);
      Assert.AreEqual(15, range.EndDay);
    }
  }
}
