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
        Food GetFood(Guid guid);
        Food GetFood(string name);
        void InsertFoods(IEnumerable<Food> foods);
        bool InsertFood(Food food);
        bool UpdateFood(Food food);
        bool DeleteFood(Food food);
    }
}