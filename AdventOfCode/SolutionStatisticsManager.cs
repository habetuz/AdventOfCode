using AdventOfCode.Time;
using Microsoft.Data.Sqlite;
using SharpLog;

namespace AdventOfCode
{
  internal class SolutionStatisticsManager : ISolutionSubmitter, ISolutionRetriever
  {
    private const string DB_NAME = "solutions.sqlite";
    private const string TABLE_NAME = "solutions";

    public SolutionStatisticsManager()
    {
      using (var connection = new SqliteConnection($"Data Source={DB_NAME}"))
      {
        try
        {
          connection.Open();
          var command = connection.CreateCommand();
          command.CommandText =
            @$"CREATE TABLE IF NOT EXISTS {TABLE_NAME} (
                        ""year"" INTEGER NOT NULL, 
                        ""day"" INTEGER NOT NULL, 
                        ""parse1"" INTEGER, 
                        ""parse2"" INTEGER, 
                        ""solve1"" INTEGER, 
                        ""solve2"" INTEGER, 
                        ""solution1"" TEXT, 
                        ""solution2"" TEXT,
                        PRIMARY KEY (year, day));";
          command.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
          Logging.LogFatal("SQLite execution failed!", "RUNNER", e);
        }
      }
    }

    public void SubmitTimes(Solution solution, Date date)
    {
      Solution? currentValues = Retrieve(date);

      if (currentValues.HasValue)
      {
        solution.Parse1 ??= currentValues.Value.Parse1;
        solution.Parse2 ??= currentValues.Value.Parse2;
        solution.Solve1 ??= currentValues.Value.Solve1;
        solution.Solve2 ??= currentValues.Value.Solve2;
        solution.Solution1 = currentValues.Value.Solution1;
        solution.Solution2 = currentValues.Value.Solution2;
      }
      else
      {
        solution.Solution1 = null;
        solution.Solution2 = null;
      }

      using (var connection = new SqliteConnection("Data Source=solutions.sqlite"))
      {
        try
        {
          connection.Open();
          var command = connection.CreateCommand();
          command.CommandText =
            @$"
                    REPLACE INTO {TABLE_NAME} (""year"", ""day"", ""parse1"", ""parse2"", ""solve1"", ""solve2"", ""solution1"", ""solution2"")
                        VALUES(
                            {date.Year},
                            {date.Day},
                            ""{(solution.Parse1 == null ? "null" : solution.Parse1.Value.Ticks)}"",
                            ""{(solution.Parse2 == null ? "null" : solution.Parse2.Value.Ticks)}"",
                            ""{(solution.Solve1 == null ? "null" : solution.Solve1.Value.Ticks)}"",
                            ""{(solution.Solve2 == null ? "null" : solution.Solve2.Value.Ticks)}"",
                            ""{(solution.Solution1 == null ? "null" : solution.Solution1)}"",
                            ""{(solution.Solution2 == null ? "null" : solution.Solution2)}"")";
          command.ExecuteNonQuery();
          connection.Close();
        }
        catch (SqliteException e)
        {
          Logging.LogFatal("SQLite execution failed!", "RUNNER", e);
        }
      }
    }

    public void SubmitSolutions(Solution solution, Date date)
    {
      Solution? currentValues = Retrieve(date);

      if (currentValues.HasValue)
      {
        solution.Parse1 = currentValues.Value.Parse1;
        solution.Parse2 = currentValues.Value.Parse2;
        solution.Solve1 = currentValues.Value.Solve1;
        solution.Solve2 = currentValues.Value.Solve2;
        solution.Solution1 ??= currentValues.Value.Solution1;
        solution.Solution2 ??= currentValues.Value.Solution2;
      }
      else
      {
        solution.Parse1 = null;
        solution.Parse2 = null;
        solution.Solve1 = null;
        solution.Solve2 = null;
      }

      using (var connection = new SqliteConnection("Data Source=solutions.sqlite"))
      {
        try
        {
          connection.Open();
          var command = connection.CreateCommand();
          command.CommandText =
            @$"
                    REPLACE INTO {TABLE_NAME} (""year"", ""day"", ""parse1"", ""parse2"", ""solve1"", ""solve2"", ""solution1"", ""solution2"")
                        VALUES(
                            {date.Year},
                            {date.Day},
                            ""{(solution.Parse1 == null ? "null" : solution.Parse1.Value.Ticks)}"",
                            ""{(solution.Parse2 == null ? "null" : solution.Parse2.Value.Ticks)}"",
                            ""{(solution.Solve1 == null ? "null" : solution.Solve1.Value.Ticks)}"",
                            ""{(solution.Solve2 == null ? "null" : solution.Solve2.Value.Ticks)}"",
                            ""{(solution.Solution1 == null ? "null" : solution.Solution1)}"",
                            ""{(solution.Solution2 == null ? "null" : solution.Solution2)}"")";
          command.ExecuteNonQuery();
          connection.Close();
        }
        catch (SqliteException e)
        {
          Logging.LogFatal("SQLite execution failed!", "RUNNER", e);
        }
      }
    }

    public Solution? Retrieve(Date date)
    {
      using (var connection = new SqliteConnection("Data Source=solutions.sqlite"))
      {
        try
        {
          connection.Open();
          var command = connection.CreateCommand();
          command.CommandText =
            @$"
                    SELECT * FROM {TABLE_NAME}
                        WHERE year = {date.Year}
                        AND   day  = {date.Day}";
          using (SqliteDataReader reader = command.ExecuteReader())
          {
            if (!reader.Read())
            {
              return null;
            }

            TimeSpan? parse1 = reader["parse1"].ToString() is not "" and not "null"
              ? new TimeSpan(long.Parse(reader["parse1"].ToString()!))
              : null;

            TimeSpan? parse2 = reader["parse2"].ToString() is not "" and not "null"
              ? new TimeSpan(long.Parse(reader["parse2"].ToString()!))
              : null;

            TimeSpan? solve1 = reader["solve1"].ToString() is not "" and not "null"
              ? new TimeSpan(long.Parse(reader["solve1"].ToString()!))
              : null;

            TimeSpan? solve2 = reader["solve2"].ToString() is not "" and not "null"
              ? new TimeSpan(long.Parse(reader["solve2"].ToString()!))
              : null;

            string? solution1 = reader["solution1"].ToString() is not "" and not "null"
              ? reader["solution1"].ToString()!
              : null;

            string? solution2 = reader["solution2"].ToString() is not "" and not "null"
              ? reader["solution2"].ToString()!
              : null;

            return new Solution()
            {
              Parse1 = parse1,
              Parse2 = parse2,
              Solve1 = solve1,
              Solve2 = solve2,
              Solution1 = solution1,
              Solution2 = solution2,
            };
          }
        }
        catch (SqliteException e)
        {
          Logging.LogFatal("SQLite execution failed!", "RUNNER", e);
          return null;
        }
      }
    }

    public void DropStatistics()
    {
      using (var connection = new SqliteConnection("Data Source=solutions.sqlite"))
      {
        try
        {
          connection.Open();
          var command = connection.CreateCommand();
          command.CommandText = @$"DROP TABLE IF EXISTS {TABLE_NAME}";
          command.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
          Logging.LogFatal("SQLite execution failed!", "RUNNER", e);
        }
      }
    }
  }
}
