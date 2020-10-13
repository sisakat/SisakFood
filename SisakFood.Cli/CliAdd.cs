using System;
using System.Collections.Generic;
using System.Linq;
using SisakCla.Core;
using SisakFood.Data.Dao;
using SisakFood.Data.Models;

namespace SisakFood.Cli
{
    public class CliAdd : CliCommand
    {
        public CliAdd(string[] args, string folder) : base(args, folder)
        {
            cli.Description = "Add foods and daily meals";
            cli.AddFunctionClass(this);
        }

        [CliOption("meal", Description = "Create new meal")]
        public void AddMeal(string name, int quantity = 1)
        {
            if (String.IsNullOrEmpty(name))
            {
                Console.WriteLine("Please provide a food name and quantity.");
                return;
            }

            var food = dao.GetFood(name);

            if (food == null)
            {
                Console.WriteLine("Specified food not found.");
                return;
            }

            Meal m = new Meal();
            m.Food = food;
            m.Quantity = quantity;

            var dailyMeals = dao.GetDailyMeals(DateTime.Now);
            dailyMeals.Meals.Add(m);

            dao.InsertDailyMeals(dailyMeals);
        }

        [CliOption("food", Description = "Create new food")]
        public void AddFood(string name, int carbohydrates = 0, int fat = 0, int protein = 0, int alcohol = 0)
        {
            if (name == null)
            {
                Console.WriteLine("Please provide a food name");
                return;
            }

            Food f = new Food()
            {
                Name = name,
                Nutrients = new Nutrition()
                {
                    Carbohydrates = carbohydrates,
                    Fat = fat,
                    Protein = protein,
                    Alcohol = alcohol
                }
            };

            List<Food> foods = new List<Food>();
            try
            {
                foods = dao.GetFoods().ToList();
            }
            catch (Exception) { }
            foods.Add(f);
            dao.InsertFoods(foods);
        }
    }
}