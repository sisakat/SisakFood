using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SisakFood.Web.Models;
using SisakFood.Data.Dao;
using SisakFood.Data.Models;

namespace SisakFood.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDao dao;

        public HomeController(ILogger<HomeController> logger, IDao dao)
        {
            _logger = logger;
            this.dao = dao;
        }

        public IActionResult Index(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate == null) fromDate = DateTime.Now.AddDays(-4);
            if (toDate == null) toDate = DateTime.Now;

            var model = new HomeModel();
            foreach (var day in EachDay(fromDate.Value, toDate.Value))
            {
                var meal = dao.GetDailyMeals(day);
                if (meal.Meals.Count > 0)
                {
                    model.DailyMeals.Add(meal);
                }
            }

            model.FromDate = fromDate.Value;
            model.ToDate = toDate.Value;
            return View(model);
        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public IActionResult Foods()
        {
            var model = new FoodsModel();
            model.Foods = dao.GetFoods().ToList();

            for (char i = 'A'; i != 'Z'; i++)
            {
                model.FoodDict.Add(i, new List<Food>());
                foreach (var food in model.Foods.Where(x => x.Name.StartsWith(i)))
                {
                    model.FoodDict[i].Add(food);
                }
                if (model.FoodDict[i].Count == 0)
                    model.FoodDict.Remove(i);
            }

            return View(model);
        }

        public IActionResult FoodEditor(Guid? id)
        {
            if (id.HasValue)
            {
                var food = dao.GetFood(id.Value);
                return View(food);
            }
            else
            {
                var model = new Food();
                model.Nutrients = new Nutrition();
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult FoodEditor(Food food)
        {
            if (ModelState.IsValid)
            {
                dao.InsertFood(food);
            }

            return RedirectToAction(nameof(Foods));
        }

        [HttpGet]
        public IActionResult FoodDelete(Guid? id)
        {
            if (id.HasValue)
            {
                var food = dao.GetFood(id.Value);
                if (food != null)
                {
                    dao.DeleteFood(food);
                }
            }

            return RedirectToAction(nameof(Foods));
        }

        [HttpGet]
        [Route("Home/MealEditor/{id:Guid}")]
        public IActionResult MealEditor(Guid id, int? qty)
        {
            var model = new MealModel();
            var food = dao.GetFood(id);
            model.FoodGuid = id;
            model.QuantitiesList = food.QuantitiesList;
            if (qty.HasValue)
            {
                model.Quantity = qty.Value;
            }
            return View(model);
        }

        public IActionResult MealEditorAt(DateTime at)
        {
            var meal = dao.GetMealFromDailyMeals(at);
            if (meal != null)
            {
                var model = new MealModel();
                var food = dao.GetFood(meal.FoodGuid);
                model.FoodGuid = meal.FoodGuid;
                model.At = meal.At;
                model.Quantity = meal.Quantity;
                model.QuantitiesList = food.QuantitiesList;
                return View(nameof(MealEditor), model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult MealEditorAt(MealModel model)
        {
            return MealEditor(model);
        }

        [HttpPost]
        public IActionResult MealEditor(MealModel model)
        {
            if (ModelState.IsValid)
            {
                Meal meal = new Meal();
                meal.Food = dao.GetFood(model.FoodGuid);
                meal.At = model.At;
                meal.Quantity = model.Quantity;
                var dailyMeals = dao.GetDailyMeals(meal.At);
                var mealFromDailyMeals = dao.GetMealFromDailyMeals(meal.At);
                if (mealFromDailyMeals != null)
                {
                    dailyMeals.Meals.Remove(mealFromDailyMeals);
                }
                dailyMeals.Meals.Add(meal);
                dao.UpdateDailyMeals(dailyMeals);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult MealDelete(DateTime at)
        {
            var dailyMeals = dao.GetDailyMeals(at);
            var mealFromDailyMeals = dao.GetMealFromDailyMeals(at);
            if (mealFromDailyMeals != null)
            {
                dailyMeals.Meals.Remove(mealFromDailyMeals);
            }
            dao.UpdateDailyMeals(dailyMeals);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("calorieDistribution")]
        public ActionResult<List<int>> GetNutrients(DateTime at)
        {
            var dailyMeals = dao.GetDailyMeals(at);
            if (dailyMeals.Meals.Count > 0)
            {
                var calories = (int)dailyMeals.CalculateKiloCalories();
                var protein = dailyMeals.CalculateProtein() * KiloCalories.PROTEIN / calories * 100;
                var carbohydrates = dailyMeals.CalculateCarbohydrates() * KiloCalories.CARBOHYDRATES / calories * 100;
                var fat = dailyMeals.CalculateFat() * KiloCalories.FAT / calories * 100;
                return new List<int>() { (int)protein, (int)carbohydrates, (int)fat };
            }

            return NotFound();
        }

        public class CalorieSummary
        {
            public DateTime Day { get; set; }
            public int Calories { get; set; }
        }

        [HttpGet("calorieSummary")]
        public ActionResult<List<CalorieSummary>> GetCalorieSummary(DateTime from, DateTime to)
        {
            List<DailyMeals> meals = new List<DailyMeals>();
            foreach (var day in EachDay(from, to))
            {
                meals.Add(dao.GetDailyMeals(day));
            }

            List<CalorieSummary> summary = new List<CalorieSummary>();
            foreach (var meal in meals)
            {
                summary.Add(new CalorieSummary
                {
                    Day = meal.Day,
                    Calories = (int)meal.CalculateKiloCalories()
                });
            }

            return summary;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
