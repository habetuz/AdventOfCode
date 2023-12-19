using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D19;

public struct RangeParts
{
    public BigRange X { get; set; }
    public BigRange M { get; set; }
    public BigRange A { get; set; }
    public BigRange S { get; set; }
    public long Combinations
    {
        get => X.Count * M.Count * A.Count * S.Count;
    }

    public BigRange this[Category category]
    {
        get
        {
            return category switch
            {
                Category.ExtremelyCool => X,
                Category.Musical => M,
                Category.Aerodynamic => A,
                Category.Shiny => S,
                _ => throw new NotImplementedException(),
            };
        }
        set
        {
            switch (category)
            {
                case Category.ExtremelyCool:
                    X = value;
                    break;
                case Category.Musical:
                    M = value;
                    break;
                case Category.Aerodynamic:
                    A = value;
                    break;
                case Category.Shiny:
                    S = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, null);
            }
        }
    }
}
