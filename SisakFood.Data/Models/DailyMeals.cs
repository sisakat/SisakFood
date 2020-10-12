using System;
using System.Collections.Generic;
using System.Linq;

namespace SisakFood.Data.Models
{
    public class DailyMeals
    {
        public List<Meal> Meals { get; set; } = new List<Meal>();
        public DateTime Day { get; set; } = DateTime.Now.Date;

        public int CalculateKiloCalories() => Meals.Sum(x => x.CalculateKiloCalories());
        public int CalculateCarbohydrates() => Meals.Sum(x => x.CalculateCarbohydrates());
        public int CalculateFat() => Meals.Sum(x => x.CalculateFat());
        public int CalculateProtein() => Meals.Sum(x => x.CalculateProtein());
        public int CalculateAlcohol() => Meals.Sum(x => x.CalculateAlcohol());
    }
}