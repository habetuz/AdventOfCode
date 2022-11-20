// <copyright file="Scanner.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D19
{
    internal class Scanner
    {
        private Beacon[] beacons = new Beacon[0];

        private int x = 0;
        private int y = 0;
        private int z = 0;

        private string xDir = "+X";
        private string yDir = "+Y";
        private string zDir = "+Z";

        internal Beacon[] Beacons { get => this.beacons; set => this.beacons = value; }

        internal int X { get => this.x; set => this.x = value; }

        internal int Y { get => this.y; set => this.y = value; }

        internal int Z { get => this.z; set => this.z = value; }

        internal string XDir { get => this.xDir; set => this.xDir = value; }

        internal string YDir { get => this.yDir; set => this.yDir = value; }

        internal string ZDir { get => this.zDir; set => this.zDir = value; }
    }
}
