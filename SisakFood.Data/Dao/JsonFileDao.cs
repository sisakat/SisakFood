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

        private void WriteJson<T>(string fileName, T value) {
            if (File.Exists(fileName)) File.Delete(fileName);
            string json = JsonSerializer.Serialize(value, new JsonSerializerOptions() {
                WriteIndented = true
            });
            File.WriteAllText(fileName, json);
        }

        public DailyMeals GetDailyMeals(DateTime from)
        {
            string fileName = GetJsonFileName(from);
            if (File.Exists(fileName))
                return ReadJson<DailyMeals>(fileName);
            else
                return new DailyMeals();
        }

        public void InsertDailyMeals(DailyMeals dailyMeals)
        {
            string fileName = GetJsonFileName(dailyMeals.Day);
            WriteJson(fileName, dailyMeals);
        }

        public void UpdateDailyMeals(DailyMeals dailyMeals)
        {
            throw new NotImplementedException("Update not defined for JSON dao");
        }

        public void DeleteDailyMeals(DailyMeals dailyMeals)
        {
            throw new NotImplementedException();
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

        public void InsertFood(Food food)
        {
            throw new NotImplementedException();
        }

        public void UpdateFood(Food food)
        {
            throw new NotImplementedException();
        }

        public void DeleteFood(Food food)
        {
            throw new NotImplementedException();
        }
    }
}