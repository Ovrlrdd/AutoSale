using AutoSale.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AutoSale.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //_logger = logger;
        //}

        ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Cars.ToListAsync());
        }

        public async Task<IActionResult> Privacy()
        {
            return View(await db.Cars.ToListAsync());
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Cars cars)
        {
            db.Cars.Add(cars);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Cars? cars = await db.Cars.FirstOrDefaultAsync(p => p.id == id);
                if (cars != null)
                {
                    db.Cars.Remove(cars);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Edit (int? id)
        { 
            if (id != null)
            {
                Cars? cars = await db.Cars.FirstOrDefaultAsync(p => p.id == id);
                if (cars != null)
                {
                    return View(cars);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit (Cars cars)
        {
            db.Cars.Update(cars);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Find (string searching)
        {
            var cars = from s in db.Cars
                       select s;
            if (!String.IsNullOrEmpty(searching))
            {
                cars = cars.Where(s => s.CarBrand.Contains(searching));
            }

            return View(cars.ToList());
        }
    }
}
