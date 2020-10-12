using System;

namespace SisakFood.Data.Models
{
    public class Meal
    {
        public Food Food { get; set; }
        public DateTime At { get; set; } = DateTime.Now;
        public int Quantity { get; set; }

        public int CalculateKiloCalories() {
            return Food.Nutrients.CalculateKiloCalories() * Quantity;
        }
    }
}