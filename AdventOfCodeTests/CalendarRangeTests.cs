using AdventOfCode.Time;

namespace AdventOfCodeTests;

[TestClass]
public class CalendarRangeTests
{
  [TestMethod]
  public void Constructor_WithDates_SetsStartAndEndDate()
  {
    Date start = new(2023, 1);
    Date end = new(2023, 25);
    CalendarRange range = new(start, end);

    Assert.AreEqual(start, range.StartDate);
    Assert.AreEqual(end, range.EndDate);
  }

  [TestMethod]
  public void Constructor_Empty_CreatesDefaultRange()
  {
    CalendarRange range = new();
    Assert.IsNotNull(range);
  }

  [TestMethod]
  public void Full_ReturnsRangeFromFirstToLast()
  {
    CalendarRange range = CalendarRange.Full;

    Assert.AreEqual(Date.First, range.StartDate);
    Assert.AreEqual(Date.Last, range.EndDate);
  }

  [TestMethod]
  public void StartDay_GetterAndInit_WorkCorrectly()
  {
    CalendarRange range = new() { StartDay = 10 };
    Assert.AreEqual(10, range.StartDay);
  }

  [TestMethod]
  public void EndDay_GetterAndInit_WorkCorrectly()
  {
    CalendarRange range = new() { EndDay = 20 };
    Assert.AreEqual(20, range.EndDay);
  }

  [TestMethod]
  public void StartYear_GetterAndInit_WorkCorrectly()
  {
    CalendarRange range = new() { StartYear = 2022 };
    Assert.AreEqual(2022, range.StartYear);
  }

  [TestMethod]
  public void EndYear_GetterAndInit_WorkCorrectly()
  {
    CalendarRange range = new() { EndYear = 2024 };
    Assert.AreEqual(2024, range.EndYear);
  }

  [TestMethod]
  public void GetEnumerator_SingleYear_IteratesCorrectly()
  {
    Date start = new(2023, 1);
    Date end = new(2023, 5);
    CalendarRange range = new(start, end);

    List<Date> dates = range.ToList();

    Assert.HasCount(5, dates);
    Assert.AreEqual(new Date(2023, 1), dates[0]);
    Assert.AreEqual(new Date(2023, 2), dates[1]);
    Assert.AreEqual(new Date(2023, 3), dates[2]);
    Assert.AreEqual(new Date(2023, 4), dates[3]);
    Assert.AreEqual(new Date(2023, 5), dates[4]);
  }

  [TestMethod]
  public void GetEnumerator_MultipleYears_IteratesCorrectly()
  {
    Date start = new(2023, 23);
    Date end = new(2024, 3);
    CalendarRange range = new(start, end);

    List<Date> dates = range.ToList();

    Assert.HasCount(6, dates);
    Assert.AreEqual(new Date(2023, 23), dates[0]);
    Assert.AreEqual(new Date(2023, 24), dates[1]);
    Assert.AreEqual(new Date(2023, 25), dates[2]);
    Assert.AreEqual(new Date(2024, 1), dates[3]);
    Assert.AreEqual(new Date(2024, 2), dates[4]);
    Assert.AreEqual(new Date(2024, 3), dates[5]);
  }

  [TestMethod]
  public void GetEnumerator_CrossingToYear2025_WrapsAtDay12()
  {
    Date start = new(2024, 23);
    Date end = new(2025, 3);
    CalendarRange range = new(start, end);

    List<Date> dates = range.ToList();

    Assert.HasCount(6, dates);
    Assert.AreEqual(new Date(2024, 23), dates[0]);
    Assert.AreEqual(new Date(2024, 24), dates[1]);
    Assert.AreEqual(new Date(2024, 25), dates[2]);
    Assert.AreEqual(new Date(2025, 1), dates[3]);
    Assert.AreEqual(new Date(2025, 2), dates[4]);
    Assert.AreEqual(new Date(2025, 3), dates[5]);
  }

  [TestMethod]
  public void GetEnumerator_Year2025CrossingDay12_WrapsCorrectly()
  {
    Date start = new(2025, 10);
    Date end = new(2026, 2);
    CalendarRange range = new(start, end);

    List<Date> dates = range.ToList();

    Assert.HasCount(5, dates);
    Assert.AreEqual(new Date(2025, 10), dates[0]);
    Assert.AreEqual(new Date(2025, 11), dates[1]);
    Assert.AreEqual(new Date(2025, 12), dates[2]);
    Assert.AreEqual(new Date(2026, 1), dates[3]);
    Assert.AreEqual(new Date(2026, 2), dates[4]);
  }

  [TestMethod]
  public void GetEnumerator_SingleDate_ReturnsSingleElement()
  {
    Date date = new(2023, 10);
    CalendarRange range = new(date, date);

    List<Date> dates = range.ToList();

    Assert.HasCount(1, dates);
    Assert.AreEqual(date, dates[0]);
  }

  [TestMethod]
  public void GetEnumerator_FullRange_IteratesFromFirstToLast()
  {
    CalendarRange range = CalendarRange.Full;
    List<Date> dates = range.ToList();

    Assert.IsGreaterThan(0, dates.Count);
    Assert.AreEqual(Date.First, dates[0]);
    Assert.AreEqual(Date.Last, dates[^1]);
  }

  [TestMethod]
  public void GetEnumerator_CanBeUsedInForeach()
  {
    Date start = new(2023, 1);
    Date end = new(2023, 3);
    CalendarRange range = new(start, end);

    int count = 0;
    foreach (Date date in range)
    {
      count++;
      Assert.IsTrue(date >= start && date <= end);
    }

    Assert.AreEqual(3, count);
  }

  [TestMethod]
  public void CalendarRangeEnumerator_Reset_RestartsIteration()
  {
    Date start = new(2023, 1);
    Date end = new(2023, 3);
    CalendarRange range = new(start, end);

    using var enumerator = range.GetEnumerator();

    // First iteration
    enumerator.MoveNext();
    Assert.AreEqual(new Date(2023, 1), enumerator.Current);
    enumerator.MoveNext();
    Assert.AreEqual(new Date(2023, 2), enumerator.Current);

    // Reset and iterate again
    enumerator.Reset();
    enumerator.MoveNext();
    Assert.AreEqual(new Date(2023, 1), enumerator.Current);
  }

  [TestMethod]
  public void CalendarRangeEnumerator_Dispose_SuppressesFinalization()
  {
    Date start = new(2023, 1);
    Date end = new(2023, 3);
    CalendarRange range = new(start, end);

    var enumerator = range.GetEnumerator();
    enumerator.Dispose(); // Should not throw
  }
}
