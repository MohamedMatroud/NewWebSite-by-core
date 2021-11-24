using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        NewsContext db;
        public HomeController(ILogger<HomeController> logger, NewsContext context)
        {
            _logger = logger;
            db = context;
        }
        
        public IActionResult Index()
        {
            var result = db.Categories.ToList();
            return View(result);
        }
        public IActionResult Teams()
        {
            var result = db.Teammembers.ToList();
            return View(result);
        }
        public async Task<IActionResult> Details(int? id)
        {
            

            if (id == null)
            {
                return NotFound();
            }
           
            var teams = await db.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        public IActionResult Messages()
        {
            var result = db.Contacts.ToList();
            return View(result);
        }
        public IActionResult News(int id)
        {
            Category c = db.Categories.Find(id);
            ViewBag.cat = c.Name;
            var result = db.News.Where(x => x.CategoryId == id).OrderByDescending(x => x.Date).ToList();
            return View(result);
        }
        public IActionResult Privacy()
        {
           
            return View();
        }
        public IActionResult DeleteNews(int id)
        {
            var news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveContact(ContactUs model)
        {
            db.Contacts.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
