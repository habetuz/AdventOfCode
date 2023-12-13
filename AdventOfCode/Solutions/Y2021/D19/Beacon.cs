namespace AdventOfCode.Solutions.Y2021.D19;

using System.Collections.Generic;

public class Beacon
{
    public Beacon()
    {
        this.Neighbors = new Dictionary<double, Beacon>();
        this.MaxDistance = 0;
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
            X = this.X,
            Y = this.Y,
            Z = this.Z,
        };
    }
}
