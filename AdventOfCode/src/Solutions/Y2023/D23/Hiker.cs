namespace AdventOfCode.Solutions.Y2023.D23;

public class Hiker(HashSet<Node> visited, Node position, int distance = 0)
{
  public HashSet<Node> Visited { get; set; } = visited;
  public Node Position { get; set; } = position;

  public int Distance { get; set; } = distance;

  public int Walk(Node goal, char[,] map)
  {
    Visited.Add(Position);

    if (Position == goal)
    {
      // Logging.LogDebug($"Found path with distance {Distance}");
      // char[,] copy = (char[,])map.Clone();
      // foreach (var node in Visited)
      // {
      //     copy[node.Position.X, node.Position.Y] = 'O';
      // }
      // Array2D.Print(
      //     (uint)map.GetLength(0),
      //     (uint)map.GetLength(1),
      //     (x, y) =>
      //         copy[x, y] switch
      //         {
      //             'O' => "[yellow]O[/]",
      //             '#' => "[gray]#[/]",
      //             _ => "[gray].[/]"
      //         }
      // );
      return Distance;
    }

    var notVisitedNeighbors = Position.Neighbors.Where(
      (neighbor) => !Visited.Contains(neighbor.node)
    );

    if (!notVisitedNeighbors.Any())
    {
      return int.MinValue;
    }
    else if (notVisitedNeighbors.Count() == 1)
    {
      var (distance, neighbor) = notVisitedNeighbors.First();

      if (Visited.Contains(neighbor))
      {
        return int.MinValue;
      }

      Distance += distance;
      Position = neighbor;

      return Walk(goal, map);
    }
    else
    {
      return notVisitedNeighbors.Max(
        (neighbor) =>
        {
          var hiker = new Hiker(
            new HashSet<Node>(Visited),
            neighbor.node,
            Distance + neighbor.distance
          );
          return hiker.Walk(goal, map);
        }
      );
    }
  }
}
