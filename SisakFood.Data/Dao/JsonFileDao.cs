using System;
using System.Collections.Generic;
using SisakFood.Data.Models;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;

namespace SisakFood.Data.Dao
{
    public class JsonFileDao : IDao
    {
        const string JSON_FILE_EXTENSION = ".json";
        const string FOODS_FILE_NAME = "foods";
        public delegate string DateTimeConverter(DateTime dateTime);
        private string _mainFolder;
        private DateTimeConverter _dateTimeConverter;

        public JsonFileDao(string mainFolder)
        {
            _mainFolder = mainFolder;
            _dateTimeConverter = dt => dt.ToString("yyyy_MM_dd");
        }

        public JsonFileDao(string mealFolder, DateTimeConverter converter) : this(mealFolder)
        {
            _dateTimeConverter = converter;
        }

        private string GetJsonFileName(DateTime from)
        {
            return Path.Combine(_mainFolder, $"{_dateTimeConverter(from)}{JSON_FILE_EXTENSION}");
        }

        private T ReadJson<T>(string fileName)
        {
            string json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<T>(json);
        }

        private void WriteJson<T>(string fileName, T value)
        {
            if (File.Exists(fileName)) File.Delete(fileName);
            string json = JsonSerializer.Serialize(value, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            File.WriteAllText(fileName, json);
        }

        public DailyMeals GetDailyMeals(DateTime from)
        {
            string fileName = GetJsonFileName(from);
            if (File.Exists(fileName))
            {
                var dailyMeals = ReadJson<DailyMeals>(fileName);
                foreach (var meal in dailyMeals.Meals)
                {
                    meal.Food = GetFood(meal.FoodGuid);
                }
                return dailyMeals;
            }
            else
                return new DailyMeals() { Day = from };
        }

        public void InsertDailyMeals(DailyMeals dailyMeals)
        {
            string fileName = GetJsonFileName(dailyMeals.Day);
            WriteJson(fileName, dailyMeals);
        }

        public void UpdateDailyMeals(DailyMeals dailyMeals)
        {
            InsertDailyMeals(dailyMeals);
        }

        public void DeleteDailyMeals(DailyMeals dailyMeals)
        {
            string fileName = GetJsonFileName(dailyMeals.Day);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public Meal GetMealFromDailyMeals(DateTime at)
        {
            var dailyMeals = GetDailyMeals(at);
            var meal = dailyMeals.Meals.FirstOrDefault(x => x.At.TrimMilliseconds() == at.TrimMilliseconds());
            return meal;
        }

        public string GetFoodsFolder()
        {
            return Path.Combine(_mainFolder, $"{FOODS_FILE_NAME}{JSON_FILE_EXTENSION}");
        }

        public IEnumerable<Food> GetFoods()
        {
            string fileName = GetFoodsFolder();
            if (File.Exists(fileName))
                return ReadJson<IEnumerable<Food>>(fileName);
            else
                return Enumerable.Empty<Food>();
        }

        public Food GetFood(Guid id)
        {
            var foods = GetFoods();
            return foods.FirstOrDefault(x => x.Id == id);
        }

        public Food GetFood(string name)
        {
            var foods = GetFoods();
            return foods.FirstOrDefault(x => x.Name == name);
        }

        public void InsertFoods(IEnumerable<Food> foods)
        {
            string fileName = GetFoodsFolder();
            WriteJson(fileName, foods);
        }

        public bool InsertFood(Food food)
        {
            var foods = GetFoods().ToList();
            if (foods.Any(x => x.Id == food.Id))
            {
                return UpdateFood(food);
            }
            else
            {
                food.Id = Guid.NewGuid();
                foods.Add(food);
                InsertFoods(foods);
                return true;
            }
        }

        public bool UpdateFood(Food food)
        {
            var foods = GetFoods().ToList();
            var already = foods.SingleOrDefault(x => x.Id == food.Id);
            if (already != null)
            {
                foods.Remove(already);
                foods.Add(food);
                InsertFoods(foods);
                return true;
            }
            return false;
        }

        public bool DeleteFood(Food food)
        {
            var foods = GetFoods().ToList();
            var already = foods.SingleOrDefault(x => x.Id == food.Id);
            if (already != null)
            {
                foods.Remove(already);
                InsertFoods(foods);
                return true;
            }
            return false;
        }
    }
}