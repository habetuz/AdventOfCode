using AdventOfCode.Utils;

namespace AdventOfCodeTests;

[TestClass]
public class DirectionUtilsTests
{
  [TestMethod]
  public void Rotate180()
  {
    var direction = Direction.UpLeft | Direction.Down | Direction.DownLeft;
    var inverted = direction.Rotate180();
    Assert.AreEqual(Direction.DownRight | Direction.Up | Direction.UpRight, inverted);
  }

  [TestMethod]
  public void Rotate90Right()
  {
    var direction = Direction.Up | Direction.DownLeft | Direction.Down;
    var inverted = direction.Rotate90Right();
    Assert.AreEqual(Direction.Right | Direction.UpLeft | Direction.Left, inverted);
  }

  [TestMethod]
  public void Rotate90Left()
  {
    var direction = Direction.UpLeft | Direction.Down | Direction.Up;
    var inverted = direction.Rotate90Left();
    Assert.AreEqual(Direction.DownLeft | Direction.Right | Direction.Left, inverted);
  }
}
