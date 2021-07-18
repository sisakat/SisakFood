using System;
using System.Collections.Generic;
using SisakFood.Data.Models;

namespace SisakFood.Web.Models
{
    public class FoodsModel
    {
        public List<Food> Foods { get; set; } = new List<Food>();
        public Dictionary<char, List<Food>> FoodDict { get; set; } = new Dictionary<char, List<Food>>();
    }
}
