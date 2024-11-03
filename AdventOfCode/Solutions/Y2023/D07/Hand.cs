using System.Diagnostics;
using System.Text.Json.Serialization;

namespace AdventOfCode.Solutions.Y2023.D07;

/// <summary>
/// Represents a hand of cards in a card game.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="HandPart1"/> struct.
/// </remarks>
/// <param name="cards">The cards in the hand.</param>
/// <param name="bid">The bid associated with the hand.</param>
[DebuggerDisplay("Value = {Value}, Bid = {Bid}")]
public struct HandPart1(CardPart1[] cards, uint bid) : IComparable<HandPart1>
{
  private int value = -1;

  /// <summary>
  /// Gets the cards in the hand.
  /// </summary>
  public CardPart1[] Cards { get; } = cards;

  /// <summary>
  /// Gets the bid associated with the hand.
  /// </summary>
  public uint Bid { get; } = bid;

  /// <summary>
  /// Compares the current instance with another <see cref="HandPart1"/> and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other <see cref="HandPart1"/>.
  /// </summary>
  /// <param name="other">The <see cref="HandPart1"/> to compare with this instance.</param>
  /// <returns>A value that indicates the relative order of the objects being compared.</returns>
  public int CompareTo(HandPart1 other)
  {
    var comparison = Value.CompareTo(other.Value);
    if (comparison != 0)
    {
      return comparison;
    }

    for (int i = 0; i < Cards.Length; i++)
    {
      comparison = Cards[i].CompareTo(other.Cards[i]);
      if (comparison != 0)
      {
        return comparison;
      }
    }

    return 0;
  }

  private int Value
  {
    get
    {
      if (value != -1)
      {
        return value;
      }

      // Check for Five of a kind
      if (Cards.GroupBy(card => card).Any(group => group.Count() == 5))
      {
        value = 8; // Five of a kind
        return value;
      }

      // Check for Four of a kind
      if (Cards.GroupBy(card => card).Any(group => group.Count() == 4))
      {
        value = 7; // Four of a kind
        return value;
      }

      // Check for Full house
      if (
        Cards.GroupBy(card => card).Any(group => group.Count() == 3)
        && Cards.GroupBy(card => card).Any(group => group.Count() == 2)
      )
      {
        value = 6; // Full house
        return value;
      }

      // Check for Three of a kind
      if (Cards.GroupBy(card => card).Any(group => group.Count() == 3))
      {
        value = 5; // Three of a kind
        return value;
      }

      // Check for Two pair
      if (Cards.GroupBy(card => card).Count(group => group.Count() == 2) == 2)
      {
        value = 4; // Two pair
        return value;
      }

      // Check for One pair
      if (Cards.GroupBy(card => card).Any(group => group.Count() == 2))
      {
        value = 3; // One pair
        return value;
      }

      // Check for High card
      value = 2; // High card
      return value;
    }
  }
}

/// <summary>
/// Represents a hand of cards in a card game.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="HandPart2"/> struct.
/// </remarks>
/// <param name="cards">The cards in the hand.</param>
/// <param name="bid">The bid associated with the hand.</param>
[DebuggerDisplay("Value = {Value}, Bid = {Bid}")]
public struct HandPart2(CardPart2[] cards, uint bid) : IComparable<HandPart2>
{
  private int value = -1;

  /// <summary>
  /// Gets the cards in the hand.
  /// </summary>
  public CardPart2[] Cards { get; } = cards;

  /// <summary>
  /// Gets the bid associated with the hand.
  /// </summary>
  public uint Bid { get; } = bid;

  /// <summary>
  /// Compares the current instance with another <see cref="HandPart2"/> and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other <see cref="HandPart1"/>.
  /// </summary>
  /// <param name="other">The <see cref="HandPart2"/> to compare with this instance.</param>
  /// <returns>A value that indicates the relative order of the objects being compared.</returns>
  public int CompareTo(HandPart2 other)
  {
    var comparison = Value.CompareTo(other.Value);
    if (comparison != 0)
    {
      return comparison;
    }

    for (int i = 0; i < Cards.Length; i++)
    {
      comparison = Cards[i].CompareTo(other.Cards[i]);
      if (comparison != 0)
      {
        return comparison;
      }
    }

    return 0;
  }

  private int Value
  {
    get
    {
      if (value != -1)
      {
        return value;
      }

      // Check for Five of a kind
      if (Cards.GroupBy(card => card).Any(group => group.Count() == 5))
      {
        value = 8; // Five of a kind
        return value;
      }

      // Check for Four of a kind
      if (Cards.GroupBy(card => card).Any(group => group.Count() == 4))
      {
        value = Cards.Contains(CardPart2.Joker) ? 8 : 7; // Five of a kind : Four of a kind
        return value;
      }

      // Check for Full house
      if (
        Cards.GroupBy(card => card).Any(group => group.Count() == 3)
        && Cards.GroupBy(card => card).Any(group => group.Count() == 2)
      )
      {
        value = Cards.Contains(CardPart2.Joker) ? 8 : 6; // Five of a kind : Full house
        return value;
      }

      // Check for Three of a kind
      if (Cards.GroupBy(card => card).Any(group => group.Count() == 3))
      {
        var jokers = Cards.Count(card => card == CardPart2.Joker);
        value = jokers switch
        {
          0 => 5, // Three of a kind
          1 => 7, // Four of a kind
          3 => 7, // Four of a kind
          _ => throw new Exception("Invalid number of jokers"),
        };
        return value;
      }

      // Check for Two pair
      if (Cards.GroupBy(card => card).Count(group => group.Count() == 2) == 2)
      {
        var jokers = Cards.Count(card => card == CardPart2.Joker);
        value = jokers switch
        {
          0 => 4, // Two pair
          1 => 6, // Full House
          2 => 7, // Four of a kind
          _ => throw new Exception("Invalid number of jokers"),
        };
        return value;
      }

      // Check for One pair
      if (Cards.GroupBy(card => card).Any(group => group.Count() == 2))
      {
        var jokers = Cards.Count(card => card == CardPart2.Joker);
        value = jokers switch
        {
          0 => 3, // One pair
          1 => 5, // Three of a kind
          2 => 5, // Three of a kind
          _ => throw new Exception("Invalid number of jokers"),
        };
        return value;
      }

      // Check for High card
      value = Cards.Contains(CardPart2.Joker) ? 3 : 2; // One Pair : High card
      return value;
    }
  }
}
