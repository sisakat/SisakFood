using System;

namespace SisakFood.Data.Models
{
    public class Food
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Nutrition Nutrients { get; set; }
    }
}