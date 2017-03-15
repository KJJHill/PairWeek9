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
        private string connectionString;
        public ParkSQLDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public List<Park> GetParkDetails(string parkCode)
        {

            string SQL_GetParkDetails = "SELECT * FROM park where parkCode = @parkCode;";

            List<Park> results = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkDetails, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park();

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

        public List<Park> GetAllParks()
        {
            List<Park> results = new List<Park>();

            return results;
        }
    }
}