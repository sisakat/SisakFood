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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Foods()
        {
            var model = new FoodsModel();
            model.Foods = dao.GetFoods().ToList();
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
