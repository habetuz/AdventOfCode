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

    public Beacon[] Beacons { get => this.beacons; set => this.beacons = value; }

    public int X { get => this.x; set => this.x = value; }

    public int Y { get => this.y; set => this.y = value; }

    public int Z { get => this.z; set => this.z = value; }

    public string XDir { get => this.xDir; set => this.xDir = value; }

    public string YDir { get => this.yDir; set => this.yDir = value; }

    public string ZDir { get => this.zDir; set => this.zDir = value; }
}
