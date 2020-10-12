using System;
using System.Collections.Generic;
using SisakFood.Data.Models;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace SisakFood.Data.Dao
{
    public class JsonFileDao : IDao
    {
        const string JSON_FILE_EXTENSION = ".json";
        const string FOODS_FILE_NAME = "foods";
        public delegate string DateTimeConverter(DateTime dateTime);
        private string _mealFolder;
        private DateTimeConverter _dateTimeConverter;

        public JsonFileDao(string mealFolder)
        {
            _mealFolder = mealFolder;
            _dateTimeConverter = dt => dt.ToString("yyyy_MM_dd");
        }

        public JsonFileDao(string mealFolder, DateTimeConverter converter) : this(mealFolder) 
        {
            _dateTimeConverter = converter;
        }

        private string GetJsonFileName(DateTime from) 
        {
            return Path.Combine(_mealFolder, $"{_dateTimeConverter(from)}{JSON_FILE_EXTENSION}");
        }

        private async Task<T> ReadJson<T>(string fileName) 
        {
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                return await JsonSerializer.DeserializeAsync<T>(stream);
            }
        }

        private async Task WriteJson<T>(string fileName, T value) {
            using (Stream stream = new FileStream(fileName, FileMode.OpenOrCreate)) 
            {
                await JsonSerializer.SerializeAsync(stream, value,
                    new JsonSerializerOptions() {
                        WriteIndented = true
                    });
            }
        }

        public async Task<IEnumerable<DailyMeals>> GetDailyMeals(DateTime from)
        {
            string fileName = GetJsonFileName(from);
            return await ReadJson<IEnumerable<DailyMeals>>(fileName);
        }

        public async Task InsertDailyMeals(DailyMeals dailyMeals)
        {
            string fileName = GetJsonFileName(dailyMeals.Day);
            await WriteJson(fileName, dailyMeals);
        }

        public async Task UpdateDailyMeals(DailyMeals dailyMeals)
        {
            await Task.FromException(new NotImplementedException("Update not defined for JSON dao"));
        }

        public Task DeleteDailyMeals(DailyMeals dailyMeals)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Food>> GetFoods()
        {
            string fileName = $"{FOODS_FILE_NAME}{JSON_FILE_EXTENSION}";
            return await ReadJson<IEnumerable<Food>>(fileName);
        }

        public async Task InsertFoods(IEnumerable<Food> foods)
        {
            string fileName = $"{FOODS_FILE_NAME}{JSON_FILE_EXTENSION}";
            await WriteJson(fileName, foods);
        }

        public Task InsertFood(Food food)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFood(Food food)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFood(Food food)
        {
            throw new NotImplementedException();
        }
    }
}