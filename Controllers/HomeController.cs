using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HarborConnect.Models;

namespace HarborConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            var categories = context.BlogCategories.ToList();
            return View(categories);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Welcome to HarborConnect - learn more about us here.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Reach out to us anytime.";

            return View();
        }
    }
}
