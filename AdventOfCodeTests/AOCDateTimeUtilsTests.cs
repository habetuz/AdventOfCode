using AdventOfCode.Time;

namespace AdventOfCodeTests;

[TestClass]
public class AOCDateTimeUtilsTests
{
  [TestMethod]
  public void GetCurrentYear_ReturnsLastYear_BeforeDecember()
  {
    DateTime fakeDate = new(2023, 11, 15);
    int year = AOCDateTimeUtils.GetCurrentYear(fakeDate);
    Assert.AreEqual(2022, year);
  }

  [TestMethod]
  public void GetCurrentYear_ReturnsCurrentYear_InDecember()
  {
    DateTime fakeDate = new(2023, 12, 20);
    int year = AOCDateTimeUtils.GetCurrentYear(fakeDate);
    Assert.AreEqual(2023, year);
  }

  [TestMethod]
  public void GetCurrentDay_ReturnsCurrentDay_InDecember()
  {
    DateTime fakeDate = new(2023, 12, 15);
    int day = AOCDateTimeUtils.GetCurrentDay(fakeDate);
    Assert.AreEqual(15, day);
  }

  [TestMethod]
  public void GetCurrentDay_Returns25_OutsideDecember()
  {
    DateTime fakeDate = new(2023, 11, 10);
    int day = AOCDateTimeUtils.GetCurrentDay(fakeDate);
    Assert.AreEqual(25, day);
  }
}
