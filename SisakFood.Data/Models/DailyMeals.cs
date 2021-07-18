using System;
using System.Collections.Generic;
using System.Linq;

namespace SisakFood.Data.Models
{
    public class DailyMeals
    {
        public List<Meal> Meals { get; set; } = new List<Meal>();
        public DateTime Day { get; set; } = DateTime.Now.Date;

        public double CalculateKiloCalories() => Meals.Sum(x => x.CalculateKiloCalories());
        public double CalculateCarbohydrates() => Meals.Sum(x => x.CalculateCarbohydrates());
        public double CalculateFat() => Meals.Sum(x => x.CalculateFat());
        public double CalculateProtein() => Meals.Sum(x => x.CalculateProtein());
        public double CalculateAlcohol() => Meals.Sum(x => x.CalculateAlcohol());
    }
}