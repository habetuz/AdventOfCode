using System.Linq;
using System.Linq.Expressions;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using SharpLog;

namespace AdventOfCode.Solutions.Y2023.D12;

public class Solver : ISolver<Line[], Line[]>
{
    public void Parse(string input, IPartSubmitter<Line[], Line[]> partSubmitter)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var result = new Line[lines.Length];
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var data = parts[0];
            var bytes = parts[1].Split(',').Select((x) => byte.Parse(x)).ToArray();

            result[i] = new Line(data, bytes);
        }
    }

    public void Solve(Line[] input, IPartSubmitter partSubmitter)
    {
        long arrangements = input.Sum(
            (line) =>
            {
                uint iterations = (uint)Math.Pow(2, line.Data.Count((c) => c == '?'));
                //Logging.LogDebug($"[yellow]============ {line.Data} ============[/]");

                uint sum = 0;

                for (uint i = 1; i <= iterations; i++)
                {
                    byte partsIndex = 0;
                    byte damagedCounter = 0;
                    byte unknownCounter = 0;
                    bool invalid = false;

                    for (byte k = 0; k < line.Data.Length; k++)
                    {
                        switch (line.Data[k])
                        {
                            case '?':
                                if ((i & (1 << unknownCounter)) != 0)
                                {
                                    if (partsIndex == line.Parts.Length)
                                    {
                                        invalid = true;
                                        break;
                                    }

                                    damagedCounter++;
                                }
                                else
                                {
                                    if (damagedCounter > 0)
                                    {
                                        if (line.Parts[partsIndex] == damagedCounter)
                                        {
                                            partsIndex++;
                                            damagedCounter = 0;
                                        }
                                        else
                                        {
                                            invalid = true;
                                        }
                                    }
                                }

                                unknownCounter++;
                                break;
                            case '#':
                                if (partsIndex == line.Parts.Length)
                                {
                                    invalid = true;
                                    break;
                                }

                                damagedCounter++;
                                break;
                            case '.':
                                if (damagedCounter > 0)
                                {
                                    if (line.Parts[partsIndex] == damagedCounter)
                                    {
                                        partsIndex++;
                                        damagedCounter = 0;
                                        break;
                                    }
                                    else
                                    {
                                        invalid = true;
                                        break;
                                    }
                                }

                                break;
                        }

                        if (invalid)
                        {
                            break;
                        }
                    }

                    if (
                        !invalid
                        && (
                            (
                                partsIndex == line.Parts.Length - 1
                                && damagedCounter == line.Parts[partsIndex]
                            ) || (partsIndex == line.Parts.Length && damagedCounter == 0)
                        )
                    )
                    {
                        sum++;
                        //Logging.LogDebug($"[green]{FormatArrangement(line.Data, i)}[/]");
                    }
                    else
                    {
                        //Logging.LogDebug(FormatArrangement(line.Data, i));
                    }
                }

                return sum;
            }
        );

        partSubmitter.SubmitPart1(arrangements);
    }

    private string FormatArrangement(string data, uint arrangement, bool valid = false)
    {
        var result = new char[data.Length];
        byte unknownCounter = 0;

        for (int i = 0; i < data.Length; i++)
        {
            switch (data[i])
            {
                case '?':
                    if ((arrangement & (1 << unknownCounter)) != 0)
                    {
                        result[i] = '#';
                    }
                    else
                    {
                        result[i] = '.';
                    }

                    unknownCounter++;
                    break;
                case '#':
                    result[i] = '#';
                    break;
                case '.':
                    result[i] = '.';
                    break;
            }
        }

        return new string(result);
    }
}