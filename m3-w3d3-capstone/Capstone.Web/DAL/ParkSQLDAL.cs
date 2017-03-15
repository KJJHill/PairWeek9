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

        string SQL_GetParkDetails = "SELECT * FROM park where parkCode = @parkCode;";
        string SQL_GetALLParks = "SELECT * FROM park;";
        private string connectionString;


        public ParkSQLDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public List<Park> GetParkDetails(string parkCode)
        {
            List<Park> results = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkDetails, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();

                    results = populateParkObject(reader);

                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
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

                    results = populateParkObject(reader);
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
        }

        private List<Park> populateParkObject(SqlDataReader reader)
        {
            List<Park> results = new List<Park>();

            while (reader.Read())
            {
                Park p = new Park();
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

                results.Add(p);
            }

            return results;

        }
    }
}