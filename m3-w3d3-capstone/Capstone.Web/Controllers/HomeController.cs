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
        string connectionString = ConfigurationManager.ConnectionStrings["KHCapstoneDatabase"].ConnectionString;

        // GET: Home
        public ActionResult Home()
        {
            IParkDAL dal = new ParkSQLDAL(connectionString);
            List<Park> result = dal.GetAllParks();

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
            List<Park> parkNames = dal.GetAllParks();

            List<SelectListItem> parks = new List<SelectListItem>();
            foreach (Park p in parkNames)
            {
                parks.Add(new SelectListItem { Text = p.ParkName, Value = p.ParkCode });
            }

            ViewBag.ParkNames = parks;

            ViewBag.ActivityLevels = activityLevel;

            ViewBag.StateNames = states;

            return View();

        }

        //GET: 
        public ActionResult SurveyResults()
        {
            ISurveyDAL dal = new SurveySQLDAL(connectionString);
            List<Survey> results = new List<Survey>();

            Survey s = new Survey();
            s.ActivityLevel = (String.IsNullOrEmpty(Request["activityLevel"]))? "Inactive": Request["activityLevel"];
            s.ParkCode = (String.IsNullOrEmpty(Request["FavoritePark"])) ? "" : Request["FavoritePark"];
            s.EmailAddress = (String.IsNullOrEmpty(Request["email"])) ? "Did not provide" : Request["email"];
            s.State = (String.IsNullOrEmpty(Request["state"])) ? "Did not provide" : Request["state"];
            dal.SaveSurvey(s);

            ViewBag.WinningPark = dal.GetWinningParkName();

            results = dal.GetAllSurveys();

            return View(results);
        }



        private List<SelectListItem> activityLevel = new List<SelectListItem>
        {
                new SelectListItem { Text = "Inactive", Value = "Inactive" },
                new SelectListItem { Text = "Sedentary", Value = "Sedentary" },
                new SelectListItem { Text = "Active", Value = "Active", Selected = true },
                new SelectListItem { Text = "Extremely Active", Value = "Extremely Active" }
        };

        private List<SelectListItem> states = new List<SelectListItem>
        {
                new SelectListItem { Text = "Alabama", Value = "Alabama" },
                new SelectListItem { Text = "Alaska", Value = "Alaska" },
                new SelectListItem { Text = "Arizona", Value = "Arizona" },
                new SelectListItem { Text = "Arkansas", Value = "Arkansas" },
                new SelectListItem { Text = "California", Value = "California" },
                new SelectListItem { Text = "Colorado", Value = "Colorado" },
                new SelectListItem { Text = "Connecticut", Value = "Connecticut" },
                new SelectListItem { Text = "Delaware", Value = "Delaware" },
                new SelectListItem { Text = "Florida", Value = "Florida" },
                new SelectListItem { Text = "Georgia", Value = "Georgia" },
                new SelectListItem { Text = "Hawaii", Value = "Hawaii" },
                new SelectListItem { Text = "Idaho", Value = "Idaho" },
                new SelectListItem { Text = "Illinois", Value = "Illinois" },
                new SelectListItem { Text = "Indiana", Value = "Indiana" },
                new SelectListItem { Text = "Iowa", Value = "Iowa" },
                new SelectListItem { Text = "Kansas", Value = "Kansas" },
                new SelectListItem { Text = "Kentucky", Value = "Kentucky" },
                new SelectListItem { Text = "Louisiana", Value = "Louisiana" },
                new SelectListItem { Text = "Maine", Value = "Maine" },
                new SelectListItem { Text = "Maryland", Value = "Maryland" },
                new SelectListItem { Text = "Massachusetts", Value = "Massachusetts" },
                new SelectListItem { Text = "Michigan", Value = "Michigan" },
                new SelectListItem { Text = "Minnesota", Value = "Minnesota" },
                new SelectListItem { Text = "Mississippi", Value = "Mississippi" },
                new SelectListItem { Text = "Missouri", Value = "Missouri" },
                new SelectListItem { Text = "Montana", Value = "Montana" },
                new SelectListItem { Text = "Nebraska", Value = "Nebraska" },
                new SelectListItem { Text = "Nevada", Value = "Nevada" },
                new SelectListItem { Text = "New Hampshire", Value = "New Hampshire" },
                new SelectListItem { Text = "New Jersey", Value = "New Jersey" },
                new SelectListItem { Text = "New Mexico", Value = "New Mexico" },
                new SelectListItem { Text = "New York", Value = "New York" },
                new SelectListItem { Text = "North Carolina", Value = "North Carolina" },
                new SelectListItem { Text = "North Dakota", Value = "North Dakota" },
                new SelectListItem { Text = "Ohio", Value = "Ohio" },
                new SelectListItem { Text = "Oklahoma", Value = "Oklahoma" },
                new SelectListItem { Text = "Oregon", Value = "Oregon" },
                new SelectListItem { Text = "Pennsylvania", Value = "Pennsylvania" },
                new SelectListItem { Text = "Rhode Island", Value = "Rhode Island" },
                new SelectListItem { Text = "South Carolina", Value = "South Carolina" },
                new SelectListItem { Text = "South Dakota", Value = "South Dakota" },
                new SelectListItem { Text = "Tennessee", Value = "Tennessee" },
                new SelectListItem { Text = "Texas", Value = "Texas" },
                new SelectListItem { Text = "Utah", Value = "Utah" },
                new SelectListItem { Text = "Vermont", Value = "Vermont" },
                new SelectListItem { Text = "Virginia", Value = "Virginia" },
                new SelectListItem { Text = "Washington", Value = "Washington" },
                new SelectListItem { Text = "West Virginia", Value = "West Virginia" },
                new SelectListItem { Text = "Wisconsin", Value = "Wisconsin" },
                new SelectListItem { Text = "Wyoming", Value = "Wyoming" },

        };
    }
}