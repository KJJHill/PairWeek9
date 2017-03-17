using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class ForecastSQLDAL : IForecastDAL
    {

        private string connectionString;
        private string SQL_GetAllForecasts = "Select * from weather where parkCode = @parkCode;";
        public ForecastSQLDAL(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        public List<Forecast> GetForecasts(string parkCode)
        {
            List<Forecast> results = new List<Forecast>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllForecasts, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Forecast f = new Forecast();

                        f.ParkCode = Convert.ToString(reader["parkCode"]);
                        f.WeatherForecast = Convert.ToString(reader["forecast"]);
                        f.DayNumber = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        f.LowTemperature = Convert.ToInt32(reader["low"]);
                        f.HighTemperature = Convert.ToInt32(reader["high"]);

                        results.Add(f);
                    }
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
        }
    }
}