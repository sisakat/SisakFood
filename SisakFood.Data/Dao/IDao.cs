using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SisakFood.Data.Models;

namespace SisakFood.Data.Dao
{
    public interface IDao
    {
        DailyMeals GetDailyMeals(DateTime from);
        void InsertDailyMeals(DailyMeals dailyMeals);
        void UpdateDailyMeals(DailyMeals dailyMeals);
        void DeleteDailyMeals(DailyMeals dailyMeals);
        IEnumerable<Food> GetFoods();
        Food GetFood(string name);
        void InsertFoods(IEnumerable<Food> foods);
        void InsertFood(Food food);
        void UpdateFood(Food food);
        void DeleteFood(Food food);
    }
}