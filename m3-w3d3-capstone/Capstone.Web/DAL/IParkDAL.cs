using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    interface IParkDAL
    {
        List<Park> GetParkDetails(Park park);

        List<Park> GetAllParks();

    }
}
