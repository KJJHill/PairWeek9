using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Forecast
    {
        public string ParkCode { get; set; }
        public int DayNumber { get; set; }
        public int LowTemperature { get; set; }
        public int HighTemperature { get; set; }
        public string WeatherForecast { get; set; }

    }
}