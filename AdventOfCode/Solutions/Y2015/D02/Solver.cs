using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2015.D02;

public class Solver : ISolver<Surface[]>
{
    public void Parse(string input, IPartSubmitter<Surface[]> partSubmitter)
    {
        var lines = input.Split((char[])['\n'], StringSplitOptions.RemoveEmptyEntries);
        var surfaces = new Surface[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var dimensions = lines[i].Split('x');
            surfaces[i].Height = byte.Parse(dimensions[0]);
            surfaces[i].Width = byte.Parse(dimensions[1]);
            surfaces[i].Length = byte.Parse(dimensions[2]);
        }

        partSubmitter.SubmitFull(surfaces);
    }

    public void Solve(Surface[] input, IPartSubmitter partSubmitter)
    {
        uint squareFeet = 0;

        for (int i = 0; i < input.Length; i++)
        {
            var surface = input[i];
            squareFeet += surface.Area + surface.Slack;
        }
    }
}
