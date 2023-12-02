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

        public void Submit(Solution solution, Date date)
        {
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

                        return new Solution()
                        {
                            Parse1 = reader["parse1"] is not "null"
                                ? new TimeSpan(long.Parse(reader["parse1"].ToString()!))
                                : null,
                            Parse2 = reader["parse2"] is not "null"
                                ? new TimeSpan(long.Parse(reader["parse2"].ToString()!))
                                : null,
                            Solve1 = reader["solve1"] is not "null"
                                ? new TimeSpan(long.Parse(reader["solve1"].ToString()!))
                                : null,
                            Solve2 = reader["solve2"] is not "null"
                                ? new TimeSpan(long.Parse(reader["solve2"].ToString()!))
                                : null,
                            Solution1 = reader["solution1"] is not "null"
                                ? reader["solution1"].ToString()!
                                : null,
                            Solution2 = reader["solution2"] is not "null"
                                ? reader["solution1"].ToString()!
                                : null,
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
    }
}
