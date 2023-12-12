using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2015.D01;

public class Solver : ISolver<string>
{
    public void Parse(string input, IPartSubmitter<string> partSubmitter)
    {
        partSubmitter.Submit(input);
    }

    public void Solve(string input, IPartSubmitter partSubmitter)
    {
        int floor = 0;
        int firstInBasement = -1;

        for(int i = 0; i < input.Length; i++) {
            if (firstInBasement == -1 && floor < 0) {
                firstInBasement = i;
            }

            switch(input[i]) {
                case '(':
                    floor ++;
                    break;
                case ')':
                    floor --;
                    break;
            }
        }

        partSubmitter.SubmitPart1(floor);
        partSubmitter.SubmitPart2(firstInBasement);
    }
}