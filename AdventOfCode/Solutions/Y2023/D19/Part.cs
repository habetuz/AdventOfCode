namespace AdventOfCode.Solutions.Y2023.D19;

public struct Part
{
    public int X { get; set; }
    public int M { get; set; }
    public int A { get; set; }
    public int S { get; set; }

    public int Value
    {
        get => X + M + A + S;
    }

    public int this[Category category]
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
