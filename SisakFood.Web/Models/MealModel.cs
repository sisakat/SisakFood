using System;
using System.Collections.Generic;
using SisakFood.Data.Models;

namespace SisakFood.Web.Models
{
    public class MealModel
    {
        public Guid FoodGuid { get; set; }
        public DateTime At { get; set; } = DateTime.Now;
        public int Quantity { get; set; }
    }
}
