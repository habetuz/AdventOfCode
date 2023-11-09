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
                        year INTEGER NOT NULL, 
                        day INTEGER NOT NULL, 
                        parse1 INTEGER NOT NULL, 
                        parse2 INTEGER NOT NULL, 
                        solve1 INTEGER NOT NULL, 
                        solve2 INTEGER NOT NULL, 
                        solution1 TEXT NOT NULL, 
                        solution2 TEXT NOT NULL)";
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
                    INSERT OR REPLACE INTO {TABLE_NAME} (year, day, parse1, parse2, solve1, solve2, solution1, solution2)
                        VALUES(
                            {date.Year},
                            {date.Day},
                            ""{(solution.Parse1 == null ? -1 : solution.Parse1.Value.Ticks)}"",
                            ""{(solution.Parse2 == null ? -1 : solution.Parse2.Value.Ticks)}"",
                            ""{(solution.Solve1 == null ? -1 : solution.Solve1.Value.Ticks)}"",
                            ""{(solution.Solve2 == null ? -1 : solution.Solve2.Value.Ticks)}"",
                            ""{(solution.Solution1 == null ? string.Empty : solution.Solution1)}"",
                            ""{(solution.Solution2 == null ? string.Empty : solution.Solution2)}"")";
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
                        WHERE year = ${date.Year}
                        AND   day  = ${date.Day}";
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read()) 
                        {
                            return null;
                        }

                        return new Solution() {
                            Parse1 = new TimeSpan(long.Parse(reader["parse1"].ToString()!)),
                            Parse2 = new TimeSpan(long.Parse(reader["parse2"].ToString()!)),
                            Solve1 = new TimeSpan(long.Parse(reader["solve1"].ToString()!)),
                            Solve2 = new TimeSpan(long.Parse(reader["solve2"].ToString()!)),
                            Solution1 = reader["solution1"].ToString()!,
                            Solution2 = reader["solution1"].ToString()!,
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