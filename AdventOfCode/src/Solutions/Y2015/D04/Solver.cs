using System.Security.Cryptography;
using System.Text;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2015.D04;

public class Solver : ISolver<string>
{
  public void Parse(string input, IPartSubmitter<string> partSubmitter)
  {
    partSubmitter.Submit(input.Trim());
  }

  public void Solve(string input, IPartSubmitter partSubmitter)
  {
    uint number = 0;
    byte[] hash;

    do
    {
      number++;
      hash = MD5.HashData(Encoding.ASCII.GetBytes(input + number));

      if (!partSubmitter.IsPart1Complete && hash[0] == 0 && hash[1] == 0 && hash[2] <= 15)
      {
        partSubmitter.SubmitPart1(number);
      }

      if (!partSubmitter.IsPart2Complete && hash[0] == 0 && hash[1] == 0 && hash[2] == 0)
      {
        partSubmitter.SubmitPart2(number);
        break;
      }
    } while (true);
  }
}
