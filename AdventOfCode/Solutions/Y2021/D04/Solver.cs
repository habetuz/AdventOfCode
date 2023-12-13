using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2021.D04;

public class Solver : ISolver<Tuple<int[], Board[]>>
{
    private int lastCompletion = -1;
    private int drawIndex = 0;

    public delegate void NewDrawHandler(Solver sender, int draw);

    public event NewDrawHandler? NewDrawEvent;

    public void Parse(string input, IPartSubmitter<Tuple<int[], Board[]>> partSubmitter)
    {
        string[] lines = input.Split('\n');

        string[] drawsString = lines[0].Split(',');
        int[] draws = new int[drawsString.Length];
        for (int i = 0; i < drawsString.Length; i++)
        {
            draws[i] = int.Parse(drawsString[i]);
        }

        List<Board> boards = new List<Board>();

        for (int boardIndex = 2; boardIndex < lines.Length; boardIndex += 6)
        {
            int[,] boardValues = new int[5, 5];

            for (int y = 0; y < 5; y++)
            {
                string[] line = lines[boardIndex + y].Split(
                    new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries
                );

                for (int x = 0; x < 5; x++)
                {
                    boardValues[x, y] = int.Parse(line[x]);
                }
            }

            boards.Add(new Board(boardValues));
        }

        partSubmitter.Submit(new Tuple<int[], Board[]>(draws, boards.ToArray()));
    }

    public void Solve(Tuple<int[], Board[]> input, IPartSubmitter partSubmitter)
    {
        foreach (Board board in input.Item2)
        {
            board.AddToDrawEvent(this);
            board.CompletedEvent += this.BoardCompleted;
        }

        for (int i = 0; i < input.Item1.Length; i++)
        {
            int draw = input.Item1[i];
            if (this.lastCompletion != -1)
            {
                break;
            }

            this.NewDrawEvent!(this, draw);
            this.drawIndex = i;
        }

        partSubmitter.SubmitPart1(this.lastCompletion);

        for (int i = this.drawIndex + 1; i < input.Item1.Length; i++)
        {
            int draw = input.Item1[i];
            if (this.NewDrawEvent == null)
            {
                break;
            }

            this.NewDrawEvent(this, draw);
        }

        partSubmitter.SubmitPart2(this.lastCompletion);
    }

    private void BoardCompleted(Board sender, int lastDraw)
    {
        this.lastCompletion = lastDraw * sender.Value;
    }
}
