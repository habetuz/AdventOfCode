namespace AdventOfCode.Solutions.Y2015.D02;

public struct Surface
{
    private uint areaWL;
    private uint areaLH;
    private uint areaHW;

    private uint areaTotal;

    public byte Width { get; set; }
    public byte Length { get; set; }
    public byte Height { get; set; }

    public uint Area
    {
        get {
            if (areaTotal == 0) {
                areaWL = (uint)(Width * Length);
                areaLH = (uint)(Length * Height);
                areaHW = (uint)(Height * Width);
                areaTotal = 2 * (areaWL + areaLH + areaHW);
            }

            return areaTotal;
        }
    }

    public uint Slack
    {
        get {
            if (Width <= Length) {
                if (Width <= Height) {
                    return (uint)(Width * Width);
                } else {
                    //return (uint)()
                }
            }

            return 0;
        }
    }
}
