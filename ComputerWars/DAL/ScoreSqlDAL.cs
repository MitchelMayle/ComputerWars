using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComputerWars.Models;
using System.Data.SqlClient;

namespace ComputerWars.DAL
{
    public class ScoreSqlDAL : IScoreDAL
    {
        private readonly string connectionString;
        private const string SQL_SaveScore = "INSERT into scores VALUES (@playerName, @playerScore);";
        private const string SQL_GetScores = "SELECT TOP 20 * FROM scores ORDER BY playerScore DESC;";

        public ScoreSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SaveScore(Player player)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SaveScore, conn);
                    cmd.Parameters.AddWithValue("@playerName", player.Name);
                    cmd.Parameters.AddWithValue("@playerScore", player.Money);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public List<HighScore> TopScores()
        {
            List<HighScore> scores = new List<HighScore>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetScores, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        HighScore score = new HighScore()
                        {
                            Name = Convert.ToString(reader["playerName"]),
                            Score = Convert.ToInt32(reader["playerScore"]),
                        };

                        scores.Add(score);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return scores;
        }
    }
}