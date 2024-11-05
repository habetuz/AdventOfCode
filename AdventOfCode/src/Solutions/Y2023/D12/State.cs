using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Solutions.Y2023.D12;

[DebuggerDisplay(
  "{CurrentChar} | Data: {DataPointer} | Damaged: {DamagedCounter} | Group: {GroupPointer}"
)]
public struct State
{
  public byte GroupPointer { get; set; }
  public byte DataPointer { get; set; }
  public byte DamagedCounter { get; set; }
  public char CurrentChar { get; set; }

  public override int GetHashCode()
  {
    return HashCode.Combine(GroupPointer, DataPointer, DamagedCounter, CurrentChar);
  }

  public static bool operator ==(State a, State b)
  {
    return a.GroupPointer == b.GroupPointer
      && a.DataPointer == b.DataPointer
      && a.DamagedCounter == b.DamagedCounter
      && a.CurrentChar == b.CurrentChar;
  }

  public static bool operator !=(State a, State b)
  {
    return !(a == b);
  }

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    if (obj is State)
    {
      return this == (State)obj;
    }

    return base.Equals(obj);
  }
}
