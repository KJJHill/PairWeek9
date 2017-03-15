using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {

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
            return View();
        }
    }
}