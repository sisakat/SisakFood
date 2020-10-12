using System;
using System.Linq;
using SisakCla.Core;
using SisakFood.Data.Models;

namespace SisakFood.Cli
{
    public class CliRemove : CliCommand
    {
        public CliRemove(string[] args) : base(args)
        {
            cli.Description = "Remove foods and daily meals";
            cli.AddFunctionClass(this);
        }

        [CliOption("food", Description = "Create new food")]
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
    }
}