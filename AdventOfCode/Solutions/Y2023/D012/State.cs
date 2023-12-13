using System.Diagnostics;

namespace AdventOfCode.Solutions.Y2023.D12;

[DebuggerDisplay("{CurrentChar} | Data: {DataPointer} | Damaged: {DamagedCounter} | Group: {GroupPointer}")]
public struct State
{
    public byte GroupPointer { get; set; }
    public byte DataPointer { get; set; }
    public byte DamagedCounter { get; set; }
    public char CurrentChar { get; set; }
}