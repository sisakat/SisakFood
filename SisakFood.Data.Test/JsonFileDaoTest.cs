using System;
using Xunit;
using SisakFood.Data.Dao;
using SisakFood.Data.Models;

namespace SisakFood.Data.Test
{
    public class JsonFileDaoTest
    {
        [Fact]
        public void TestJson()
        {
            IDao dao = new JsonFileDao("C:\\Temp\\");
            DailyMeals dailyMeals = new DailyMeals();
            dao.InsertDailyMeals(dailyMeals);
        }
    }
}
