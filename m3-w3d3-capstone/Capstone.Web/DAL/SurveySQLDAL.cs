﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class SurveySQLDAL : ISurveyDAL
    {

        private string connectionString;
        public SurveySQLDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public List<Survey> GetAllSurveys()
        {
            List<Survey> results = new List<Survey>();

            return results;
        }

        public bool SaveSurvey(Survey survey)
        {
            bool result = false;

            return result;
        }

    }
}