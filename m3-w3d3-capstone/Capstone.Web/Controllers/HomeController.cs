using System;
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
        string connectionString = ConfigurationManager.ConnectionStrings["JRCapstoneDatabase"].ConnectionString;

        // GET: Home
        public ActionResult Home()
        {
            IParkDAL dal = new ParkSQLDAL(connectionString);
            List<Park> result =dal.GetAllParks();

            return View(result);
        }

        //GET: 
        public ActionResult DetailPage()
        {
            Park result = new Park();

            if (!string.IsNullOrEmpty(Request["parkCode"]))
            {
                IParkDAL dal = new ParkSQLDAL(connectionString);
                result = dal.GetParkDetails(Request["parkCode"]);
            }
            foreach (Forecast f in result.FiveDayForecasts)
            {
                switch (f.WeatherForecast)
                {
                    case "snow":
                        f.ForecastAdvice += "Please pack snowshoes.";
                        break;
                    case "rain":
                        f.ForecastAdvice += "Please pack raingear and wear weatherproof shoes!";
                        break;
                    case "thunderstorms":
                        f.ForecastAdvice += "Please seek shelter and avoid hiking on exposed ridges.";
                        break;
                    case "sunny":
                        f.ForecastAdvice += "Please pack sunblock. Only you can prevent forest fires.";
                        break;
                }
                if (f.HighTemperature > 75)
                {
                    f.ForecastAdvice += "  Please bring an extra gallon of water.";
                }
                if (f.LowTemperature < 20)
                {
                    f.ForecastAdvice += "  Danger, frigid temperatures!";
                }
                if (f.HighTemperature - f.LowTemperature >= 20)
                {
                    f.ForecastAdvice += "  Please wear breathable layers.";
                }
                if (f.WeatherForecast != "partly cloudy")
                {
                    f.ImagePath = "/Content/img/weatherimages/" + f.WeatherForecast + ".png";
                }
                else
                {
                    f.ImagePath = "/Content/img/weatherimages/partlycloudy.png";
                }
            }
            
            return View(result);
        }


        //GET: 
        public ActionResult DailySurvey()
        { 
         
            IParkDAL dal = new ParkSQLDAL(connectionString);

            //List<Park> parkNames = dal.GetParkNames();
            //        public static List<SelectListItem> ParkNames { get; } = new List<SelectListItem>()
            //{
            //    new SelectListItem() {Text = "Park1", Value = "1" }

            //};
            return View();

        }

        //GET: 
        public ActionResult SurveyResults()
        {
            List<Survey> results = new List<Survey>();

            ISurveyDAL dal = new SurveySQLDAL(connectionString);

            results = dal.GetAllSurveys();

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