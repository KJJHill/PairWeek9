using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class ParkSQLDAL : IParkDAL
    {
        public List<Park> GetParkDetails(Park park)
        {
            List<Park> results = new List<Park>();

            return results;
        }

        public List<Park> GetAllParks()
        {
            List<Park> results = new List<Park>();

            return results;
        }
    }
}