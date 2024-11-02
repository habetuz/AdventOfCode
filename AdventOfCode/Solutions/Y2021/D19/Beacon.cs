namespace AdventOfCode.Solutions.Y2021.D19;

using System.Collections.Generic;

public class Beacon
{
  public Beacon()
  {
    Neighbors = [];
    MaxDistance = 0;
  }

  public int X { get; set; }

  public int Y { get; set; }

  public int Z { get; set; }

  public Dictionary<double, Beacon> Neighbors { get; set; }

  public double MaxDistance { get; set; }

  public Beacon Clone()
  {
    return new Beacon
    {
      X = X,
      Y = Y,
      Z = Z,
    };
  }
}
