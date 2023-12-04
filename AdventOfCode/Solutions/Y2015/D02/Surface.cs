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
        get
        {
            if (areaTotal == 0)
            {
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
        get
        {
            if (areaTotal == 0)
            {
                areaWL = (uint)(Width * Length);
                areaLH = (uint)(Length * Height);
                areaHW = (uint)(Height * Width);
                areaTotal = 2 * (areaWL + areaLH + areaHW);
            }

            return Math.Min(areaWL, Math.Min(areaLH, areaHW));
        }
    }

    public uint Bow
    {
        get
        {
            byte[] dimensions = new byte[] { Width, Length, Height };
            Array.Sort(dimensions);
            byte smallest1 = dimensions[0];
            byte smallest2 = dimensions[1];
            uint ribbon = (uint)(2 * (smallest1 + smallest2));
            ribbon += (uint)(Width * Length * Height);
            return ribbon;
        }
    }
}
