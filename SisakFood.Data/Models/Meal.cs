using System;

namespace SisakFood.Data.Models
{
    public class Meal
    {
        public Food Food { get; set; }
        public DateTime At { get; set; } = DateTime.Now;
        public int Quantity { get; set; }

        public int CalculateKiloCalories() {
            return Food.Nutrients.CalculateKiloCalories() * Quantity / 100;
        }

        public int CalculateCarbohydrates() => Food.Nutrients.Carbohydrates * Quantity / 100;
        public int CalculateFat() => Food.Nutrients.Fat * Quantity / 100;
        public int CalculateProtein() => Food.Nutrients.Protein * Quantity / 100;
        public int CalculateAlcohol() => Food.Nutrients.Alcohol * Quantity / 100;
    }
}