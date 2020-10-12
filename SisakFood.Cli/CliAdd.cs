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
        public CliAdd(string[] args) : base(args)
        {
            cli.Description = "Add foods and daily meals";
            cli.AddFunctionClass(this);
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
                foods = dao.GetFoods().Result.ToList();
            }
            catch (Exception) { }
            foods.Add(f);
            dao.InsertFoods(foods);
        }
    }
}