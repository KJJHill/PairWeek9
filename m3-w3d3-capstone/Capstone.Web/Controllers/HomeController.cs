﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["KHCapstoneDatabase"].ConnectionString;

        // GET: Home
        public ActionResult Home()
        {
            return View();
        }

        //GET: 
        public ActionResult DetailPage()
        {
            return View();
        }


        //GET: 
        public ActionResult DailySurvey()
        {
            return View();
        }

        //GET: 
        public ActionResult SurveyResults()
        {
            return View();
        }


        //GET: 
        public ActionResult FiveDayForecast()
        {
            List<Forecast> results = new List<Forecast>();

            IForecastDAL dal = new ForecastSQLDAL(connectionString);

            results = dal.GetForecasts("ENP");

            return View(results);
        }
    }
}