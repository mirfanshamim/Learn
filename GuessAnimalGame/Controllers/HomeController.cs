using GuessAnimalGame.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GuessAnimalGame.Controllers
{
    public class HomeController : Controller
    {
        private GuessAnimalEntities db = new GuessAnimalEntities();

        public ActionResult Index()
        {
            return View(db.Animals.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}