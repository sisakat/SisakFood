using System;
using System.Text.Json.Serialization;

namespace SisakFood.Data.Models
{
    public class Meal
    {
        private Food _food;

        [JsonIgnore]
        public Food Food
        {
            get
            {
                return _food;
            }

            set
            {
                _food = value;
                FoodGuid = _food.Id;
            }
        }

        public Guid FoodGuid { get; set; }
        private DateTime _at = DateTime.Now.TrimMilliseconds();
        public DateTime At
        {
            get => _at;
            set
            {
                _at = value.TrimMilliseconds();
            }
        }

        public int Quantity { get; set; }

        public double CalculateKiloCalories()
        {
            return Food.Nutrients.CalculateKiloCalories() * Quantity / 100;
        }

        public double CalculateCarbohydrates() => Food.Nutrients.Carbohydrates * Quantity / 100;
        public double CalculateFat() => Food.Nutrients.Fat * Quantity / 100;
        public double CalculateProtein() => Food.Nutrients.Protein * Quantity / 100;
        public double CalculateAlcohol() => Food.Nutrients.Alcohol * Quantity / 100;

        public override bool Equals(object other)
        {
            var o = other as Meal;
            if (o != null)
            {
                return o.At.TrimMilliseconds() == this.At.TrimMilliseconds();
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.At.GetHashCode();
        }
    }
}