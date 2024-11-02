namespace AdventOfCode.Solutions.Y2021.D19;

public class Scanner
{
    private Beacon[] beacons = new Beacon[0];

    private int x = 0;
    private int y = 0;
    private int z = 0;

    private string xDir = "+X";
    private string yDir = "+Y";
    private string zDir = "+Z";

    public Beacon[] Beacons
    {
        get => beacons;
        set => beacons = value;
    }

    public int X
    {
        get => x;
        set => x = value;
    }

    public int Y
    {
        get => y;
        set => y = value;
    }

    public int Z
    {
        get => z;
        set => z = value;
    }

    public string XDir
    {
        get => xDir;
        set => xDir = value;
    }

    public string YDir
    {
        get => yDir;
        set => yDir = value;
    }

    public string ZDir
    {
        get => zDir;
        set => zDir = value;
    }
}
