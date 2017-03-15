using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class SurveySQLDAL : ISurveyDAL
    {
        private string SQL_GetAllSurveys = "Select * from survey_result";
        private string SQL_SaveSurvey = "Insert into survey_result (parkCode, emailAddress, state, activityLevel) values (@parkCode, @emailAddress, @state, @activityLevel);";

        private string connectionString;

        public SurveySQLDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public List<Survey> GetAllSurveys()
        {
            List<Survey> results = new List<Survey>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllSurveys, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Survey s = new Survey();

                        s.SurveyId = Convert.ToInt32(reader["surveyId"]);
                        s.ParkCode = Convert.ToString(reader["parkCode"]);
                        s.EmailAddress = Convert.ToString(reader["emailAddress"]);
                        s.State = Convert.ToString(reader["state"]);
                        s.ActivityLevel = Convert.ToString(reader["activityLevel"]);

                        results.Add(s);
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

        public bool SaveSurvey(Survey survey)
        {
            bool result = false;
            int rowAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllSurveys, conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    rowAffected = cmd.ExecuteNonQuery();
                }

                result = (rowAffected == 1) ? true: false;
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return result;
        }

    }
}