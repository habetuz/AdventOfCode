﻿namespace AdventOfCode.Solutions.Y2021.D04;

public class Board
{
  private readonly int[,] boardValues;
  private readonly bool[,] boardChecked = new bool[5, 5];

  internal Board(int[,] boardValues)
  {
    this.boardValues = boardValues;
  }

  internal delegate void CompletedHandler(Board sender, int lastDraw);

  internal event CompletedHandler? CompletedEvent;

  internal int Value
  {
    get
    {
      int value = 0;
      for (int x = 0; x < boardValues.GetLength(0); x++)
      {
        for (int y = 0; y < boardValues.GetLength(1); y++)
        {
          if (!boardChecked[x, y])
          {
            value += boardValues[x, y];
          }
        }
      }

      return value;
    }
  }

  internal void AddToDrawEvent(Solver solution)
  {
    solution.NewDrawEvent += Draw;
  }

  private void Draw(Solver sender, int draw)
  {
    for (int x = 0; x < boardValues.GetLength(0); x++)
    {
      for (int y = 0; y < boardValues.GetLength(1); y++)
      {
        if (boardValues[x, y] == draw)
        {
          boardChecked[x, y] = true;
          if (IsCompleted(x, y))
          {
            sender.NewDrawEvent -= Draw;
            CompletedEvent!(this, draw);
          }
        }
      }
    }
  }

  private bool IsCompleted(int x, int y)
  {
    bool xComplete = true;
    bool yComplete = true;

    for (int i = 0; i < boardValues.GetLength(0); i++)
    {
      if (!boardChecked[x, i])
      {
        yComplete = false;
      }

      if (!boardChecked[i, y])
      {
        xComplete = false;
      }
    }

    return xComplete || yComplete;
  }
}
