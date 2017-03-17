using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class ParkSQLDAL : IParkDAL
    {
        private string SQL_GetAllForecasts = "Select * from weather where parkCode = @parkCode;";
        string SQL_GetParkDetails = "SELECT * FROM park where parkCode = @parkCode;";
        string SQL_GetALLParks = "SELECT * FROM park;";
        string SQL_GetParkNames = "SELECT parkName FROM park;";
        private string connectionString;


        public ParkSQLDAL(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        public Park GetParkDetails(string parkCode)
        {
            Park p = new Park();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkDetails, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        p.ParkCode = Convert.ToString(reader["parkCode"]);
                        p.ParkName = Convert.ToString(reader["parkName"]);
                        p.State = Convert.ToString(reader["state"]);
                        p.Acreage = Convert.ToInt32(reader["acreage"]);
                        p.Elevation = Convert.ToInt32(reader["elevationInFeet"]);
                        p.MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]);
                        p.NumberOfCampsite = Convert.ToInt32(reader["numberOfCampsites"]);
                        p.Climate = Convert.ToString(reader["climate"]);
                        p.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        p.AnnualVisitors = Convert.ToInt32(reader["annualVisitorCount"]);
                        p.Quote = Convert.ToString(reader["inspirationalQuote"]);
                        p.QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        p.Description = Convert.ToString(reader["parkDescription"]);
                        p.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        p.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                    }

                    reader.Close();

                    cmd = new SqlCommand(SQL_GetAllForecasts, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    reader = cmd.ExecuteReader();

                    List<Forecast> forecasts = new List<Forecast>();

                    while (reader.Read())
                    {
                        Forecast f = new Forecast();

                        f.WeatherForecast = Convert.ToString(reader["forecast"]);
                        f.DayNumber = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        f.LowTemperature = Convert.ToInt32(reader["low"]);
                        f.HighTemperature = Convert.ToInt32(reader["high"]);

                        forecasts.Add(f);
                    }

                    p.FiveDayForecasts = forecasts;
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return p;
        }

        public List<Park> GetAllParks()
        {
            List<Park> results = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetALLParks, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park();
                        p.ParkCode = Convert.ToString(reader["parkCode"]);
                        p.ParkName = Convert.ToString(reader["parkName"]);
                        p.State = Convert.ToString(reader["state"]);
                        p.Description = Convert.ToString(reader["parkDescription"]);
                        results.Add(p);
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

        public List<string> GetParkNames()
        {
            List<string> results = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkNames, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        results.Add(Convert.ToString(reader["parkName"]));
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