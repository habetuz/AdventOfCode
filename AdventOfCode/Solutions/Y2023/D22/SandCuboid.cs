using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D22;

public class SandCuboid : Cuboid
{
  private int removedSupports = 0;

  public SandCuboid(Coordinate3D a, Coordinate3D b)
    : base(a, b) { }

  public SandCuboid(Coordinate3D position, long xLength, long yLength, long zLength)
    : base(position, xLength, yLength, zLength) { }

  public HashSet<SandCuboid> SupportedBy { get; } = [];
  public HashSet<SandCuboid> Supporting { get; } = [];

  public int RemoveSupport()
  {
    removedSupports++;
    if (removedSupports == SupportedBy.Count)
    {
      return RemoveSelf() + 1;
    }

    return 0;
  }

  public int RemoveSelf()
  {
    int fallCount = 0;
    foreach (var cuboid in Supporting)
    {
      fallCount += cuboid.RemoveSupport();
    }

    return fallCount;
  }

  public void Reset()
  {
    removedSupports = 0;
    foreach (var cuboid in Supporting)
    {
      cuboid.Reset();
    }
  }
}
