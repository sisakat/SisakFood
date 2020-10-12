using System;
using System.Collections.Generic;
using System.Linq;

namespace SisakFood.Data.Models
{
    public class DailyMeals
    {
        public IEnumerable<Meal> Meals { get; set; } = Enumerable.Empty<Meal>();
        public DateTime Day { get; set; } = DateTime.Now.Date;
    }
}