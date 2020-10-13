using System;
using System.Linq;
using SisakCla.Core;
using SisakFood.Data.Models;

namespace SisakFood.Cli
{
    public class CliRemove : CliCommand
    {
        public CliRemove(string[] args, string folder) : base(args, folder)
        {
            cli.Description = "Remove foods and daily meals";
            cli.AddFunctionClass(this);
        }

        [CliOption("food", Description = "Remove food")]
        public void RemoveFood(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Please provide a name");
                return;
            }

            var foods = (dao.GetFoods()).ToList();
            int removed = foods.RemoveAll(x => x.Name == name);
            Console.WriteLine($"{removed} foods removed");
            dao.InsertFoods(foods);
            Console.WriteLine("Foods saved");
        }

        [CliOption("meal", Description = "Remove meal")]
        public void RemoveMeal(string name, DateTime date)
        {
            var dailyMeals = dao.GetDailyMeals(date);
            int count = dailyMeals.Meals.RemoveAll(x => x.Food.Name == name);
            Console.WriteLine($"{count} item removed");
            dao.InsertDailyMeals(dailyMeals);
        }
    }
}