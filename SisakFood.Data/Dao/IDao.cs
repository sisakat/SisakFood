using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SisakFood.Data.Models;

namespace SisakFood.Data.Dao
{
    public interface IDao
    {
        Task<IEnumerable<DailyMeals>> GetDailyMeals(DateTime from);
        Task InsertDailyMeals(DailyMeals dailyMeals);
        Task UpdateDailyMeals(DailyMeals dailyMeals);
        Task DeleteDailyMeals(DailyMeals dailyMeals);
        Task<IEnumerable<Food>> GetFoods();
        Task InsertFoods(IEnumerable<Food> foods);
        Task InsertFood(Food food);
        Task UpdateFood(Food food);
        Task DeleteFood(Food food);
    }
}