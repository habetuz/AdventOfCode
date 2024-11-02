using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2023.D07;

public class Solver : ISolver<HandPart1[], HandPart2[]>
{
    public void Parse(string input, IPartSubmitter<HandPart1[], HandPart2[]> partSubmitter)
    {
        string[][] lines = input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(l => l.Split(' '))
            .ToArray();
        HandPart1[] handsPart1 = new HandPart1[lines.Length];
        HandPart2[] handsPart2 = new HandPart2[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            CardPart1[] cardsPart1 = new CardPart1[lines[i][0].Length];
            CardPart2[] cardsPart2 = new CardPart2[lines[i][0].Length];
            for (int j = 0; j < cardsPart1.Length; j++)
            {
                cardsPart1[j] = lines[i][0][j] switch
                {
                    '2' => CardPart1.Two,
                    '3' => CardPart1.Three,
                    '4' => CardPart1.Four,
                    '5' => CardPart1.Five,
                    '6' => CardPart1.Six,
                    '7' => CardPart1.Seven,
                    '8' => CardPart1.Eight,
                    '9' => CardPart1.Nine,
                    'T' => CardPart1.Ten,
                    'J' => CardPart1.Jack,
                    'Q' => CardPart1.Queen,
                    'K' => CardPart1.King,
                    'A' => CardPart1.Ace,
                    _ => throw new Exception("Invalid card"),
                };

                cardsPart2[j] = lines[i][0][j] switch
                {
                    'J' => CardPart2.Joker,
                    '2' => CardPart2.Two,
                    '3' => CardPart2.Three,
                    '4' => CardPart2.Four,
                    '5' => CardPart2.Five,
                    '6' => CardPart2.Six,
                    '7' => CardPart2.Seven,
                    '8' => CardPart2.Eight,
                    '9' => CardPart2.Nine,
                    'T' => CardPart2.Ten,
                    'Q' => CardPart2.Queen,
                    'K' => CardPart2.King,
                    'A' => CardPart2.Ace,
                    _ => throw new Exception("Invalid card"),
                };
            }

            handsPart1[i] = new HandPart1(cardsPart1, uint.Parse(lines[i][1]));
            handsPart2[i] = new HandPart2(cardsPart2, uint.Parse(lines[i][1]));
        }

        partSubmitter.SubmitPart1(handsPart1);
        partSubmitter.SubmitPart2(handsPart2);
    }

    public void Solve(HandPart1[] input1, HandPart2[] input2, IPartSubmitter partSubmitter)
    {
        Array.Sort(input1);
        Array.Sort(input2);
        uint totalWinningsPart1 = 0;
        uint totalWinningsPart2 = 0;
        for (uint i = 0; i < input1.Length; i++)
        {
            totalWinningsPart1 += input1[i].Bid * (i + 1);
            totalWinningsPart2 += input2[i].Bid * (i + 1);
        }

        partSubmitter.SubmitPart1(totalWinningsPart1);
        partSubmitter.SubmitPart2(totalWinningsPart2);
    }
}
