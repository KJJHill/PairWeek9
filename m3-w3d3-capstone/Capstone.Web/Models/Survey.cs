using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    public class Survey
    {

        public int SurveyId { get; set; }
        public string ParkCode { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }
        public string State { get; set; }
        public string ActivityLevel { get; set; }
        public string ParkName { get; set; }

        
    }
}