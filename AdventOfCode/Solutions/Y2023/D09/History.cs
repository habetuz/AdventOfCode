using System.Collections;

namespace AdventOfCode.Solutions.Y2023.D09;

public class History : IEnumerable<int>, ICloneable
{
  private int[] history;

  private readonly History? subHistory;

  public History(int[] history)
  {
    this.history = history;

    int[] subHistoryArray = new int[history.Length - 1];
    for (int i = 0; i < subHistoryArray.Length; i++)
    {
      subHistoryArray[i] = history[i + 1] - history[i];
    }

    if (!subHistoryArray.All(num => num == 0))
    {
      subHistory = new History(subHistoryArray);
    }
  }

  public int PredictForward()
  {
    int[] extendedHistory = new int[history.Length + 1];
    history.CopyTo(extendedHistory, 0);
    history = extendedHistory;

    if (subHistory is not null)
    {
      history[^1] = history[^2] + subHistory.PredictForward();
    }
    else
    {
      history[^1] = history[^2];
    }

    return history[^1];
  }

  public int PredictBackward()
  {
    int[] extendedHistory = new int[history.Length + 1];
    history.CopyTo(extendedHistory, 1);
    history = extendedHistory;

    if (subHistory is not null)
    {
      history[0] = history[1] - subHistory.PredictBackward();
    }
    else
    {
      history[0] = history[1];
    }

    return history[0];
  }

  public IEnumerator<int> GetEnumerator()
  {
    return ((IEnumerable<int>)history).GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return history.GetEnumerator();
  }

  public object Clone()
  {
    return new History((int[])history.Clone());
  }
}
