using System;
using System.Collections.Generic;
using SisakFood.Data.Models;

namespace SisakFood.Web.Models
{
    public class HomeModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<DailyMeals> DailyMeals { get; set; } = new List<DailyMeals>();
    }
}
