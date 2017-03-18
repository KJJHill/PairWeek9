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
        private string SQL_GetAllSurveys = "Select surveyId, park.parkCode, emailAddress, park.state, activityLevel, parkName from survey_result join park ON park.parkCode = survey_result.parkCode";
        private string SQL_SaveSurvey = "Insert into survey_result (parkCode, emailAddress, state, activityLevel) values (@parkCode, @emailAddress, @state, @activityLevel);";
        private string SQL_GetSurveyWinnerParkCode = "select top 1 Count(parkCode) as pcCount, parkCode from survey_result group by parkCode order by pcCount desc;";
        private string SQL_GetSurveyWinnerParkName = "select parkName from park where parkCode = @parkCode";
        private string connectionString;

        public SurveySQLDAL(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
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
                        s.ParkName = Convert.ToString(reader["parkName"]);
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

                    SqlCommand cmd = new SqlCommand(SQL_SaveSurvey, conn);
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

        public string GetWinningParkName()
        {
            string winningParkCode = "";
            string winningParkName = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetSurveyWinnerParkCode, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        winningParkCode = Convert.ToString(reader["parkCode"]);
                    }

                    reader.Close();

                    cmd = new SqlCommand(SQL_GetSurveyWinnerParkName, conn);
                    cmd.Parameters.AddWithValue("@parkCode", winningParkCode);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        winningParkName = Convert.ToString(reader["parkName"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return winningParkName;
        }

    }
}