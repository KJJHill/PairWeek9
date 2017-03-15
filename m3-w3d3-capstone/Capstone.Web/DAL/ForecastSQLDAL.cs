using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class ForecastSQLDAL : IForecastDAL
    {
        public List<Forecast> GetForecasts()
        {
            List<Forecast> results = new List<Forecast>();

            return results;
        }
    }
}