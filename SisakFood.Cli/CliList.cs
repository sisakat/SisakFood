using System;
using SisakCla.Core;
using SisakFood.Data.Models;

namespace SisakFood.Cli
{
    public class CliList : CliCommand
    {
        public CliList(string[] args, string folder) : base(args, folder)
        {
            cli.Description = "List foods and meals";
            cli.AddFunctionClass(this);
        }

        [CliOption("food", LongOption = "foods", Description = "List all food")]
        public void ListFood(string pattern = "")
        {
            var foods = dao.GetFoods();
            PrintFoodHeader();
            PrintLine();
            foreach (var food in foods) 
            {
                if (string.IsNullOrEmpty(pattern) || food.Name.Contains(pattern))
                {
                    PrintFood(food);
                }
            }
        }

        [CliOption("meal", LongOption = "meals", Description = "List all meals")]
        public void ListMeals(string date)
        {
            DateTime dateTime;
            if (!DateTime.TryParse(date, out dateTime)) 
            {
                Console.WriteLine("Specified date could not be parsed. Using the current day instead.");
                dateTime = DateTime.Now;
            }

            var meals = dao.GetDailyMeals(dateTime);
            PrintFoodHeader();
            PrintLine();
            foreach (var meal in meals.Meals) 
            {
                PrintMeal(meal);
            }

            PrintLine();
            var str = String.Format("{0,-20} {1,10} kcal {2,10} g {3,10} g {4,10} g {5,10} g",
                "Total",
                meals.CalculateKiloCalories(),
                meals.CalculateCarbohydrates(),
                meals.CalculateFat(),
                meals.CalculateProtein(),
                meals.CalculateAlcohol());
            Console.WriteLine(str);
        }

        public void PrintLine()
        {
            Console.WriteLine("------------------------------------------"
                + "----------------------------------------------");
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

        public void PrintMeal(Meal meal) 
        {
            var str = String.Format("{0,-20} {1,10} kcal {2,10} g {3,10} g {4,10} g {5,10} g",
                meal.Food.Name,
                meal.CalculateKiloCalories(),
                meal.CalculateCarbohydrates(),
                meal.CalculateFat(),
                meal.CalculateProtein(),
                meal.CalculateAlcohol());
            Console.WriteLine(str);
        }
    }
}