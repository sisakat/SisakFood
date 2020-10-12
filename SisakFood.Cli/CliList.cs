using System;
using SisakCla.Core;
using SisakFood.Data.Models;

namespace SisakFood.Cli
{
    public class CliList : CliCommand
    {
        public CliList(string[] args) : base(args)
        {
            cli.Description = "List foods and meals";
            cli.AddFunctionClass(this);
        }

        [CliOption("food", Description = "List all food")]
        public void ListFood(string pattern = "")
        {
            var foods = dao.GetFoods().Result;
            PrintFoodHeader();
            foreach (var food in foods) 
            {
                if (string.IsNullOrEmpty(pattern) || food.Name.Contains(pattern))
                {
                    PrintFood(food);
                }
            }
        }

        public void PrintFoodHeader()
        {
            var str = String.Format("{0,-20} {1,15} {2,12} {3,12} {4,12} {5,12}",
                "Name",
                "Calories",
                "Carbs",
                "Fat",
                "Protein",
                "Alcohol");
            Console.WriteLine(str);
        }

        public void PrintFood(Food food)
        {
            var str = String.Format("{0,-20} {1,10} kcal {2,10} g {3,10} g {4,10} g {5,10} g",
                food.Name,
                food.Nutrients.CalculateKiloCalories(),
                food.Nutrients.Carbohydrates,
                food.Nutrients.Fat,
                food.Nutrients.Protein,
                food.Nutrients.Alcohol);
            Console.WriteLine(str);
        }
    }
}