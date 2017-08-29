using GuessAnimalGame.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GuessAnimalGame.DAL
{
    public class AnimalsController : Controller
    {
        private GuessAnimalEntities db = new GuessAnimalEntities();

        // GET: Animals
        public ActionResult Index()
        {
            return View(db.Animals.ToList());
        }

        public ActionResult GetHasQuestions()
        {
            GuessAnimalViewModel guessAnimalViewModel = new GuessAnimalViewModel();
            var list = db.Animals.ToList();

            foreach (var animal in list) {
                guessAnimalViewModel.HasQuestions.Add(animal.HasQuestion);
            }

            guessAnimalViewModel.Message = "Success";

            return Json(guessAnimalViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBehaveQuestions()
        {
            GuessAnimalViewModel guessAnimalViewModel = new GuessAnimalViewModel();
            var list = db.Animals.ToList();

            foreach (var animal in list)
            {
                guessAnimalViewModel.BehaveQuestions.Add(animal.BehaviourQuestion);
            }

            guessAnimalViewModel.Message = "Success";

            return Json(guessAnimalViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIsQuestions()
        {
            GuessAnimalViewModel guessAnimalViewModel = new GuessAnimalViewModel();
            var list = db.Animals.ToList();

            foreach (var animal in list)
            {
                guessAnimalViewModel.IsQuestions.Add(animal.IsQuestion);
            }

            guessAnimalViewModel.Message = "Success";

            return Json(guessAnimalViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckIfFound(string q)
        {
            string hasQ = "" ;
            string behaveQ = "";
            string isQ = "";

            string[] str = q.Split(',');

            if (str.Length == 1)
            {
                hasQ = str[0];
                behaveQ = str[1];
            }
            else {
                hasQ = str[0];
                behaveQ = str[1];
                isQ = str[2];
            }

            List<Animal> list;

            if (string.IsNullOrWhiteSpace(behaveQ) && string.IsNullOrWhiteSpace(isQ))
            {
                list = db.Animals.ToList().FindAll(a => a.HasQuestion == hasQ);
            }
            else if (string.IsNullOrWhiteSpace(isQ))
            {
                list = db.Animals.ToList().FindAll(a => a.HasQuestion == hasQ && a.BehaviourQuestion == behaveQ);
            }
            else {
                list = db.Animals.ToList().FindAll(a => a.HasQuestion == hasQ 
                && a.BehaviourQuestion == behaveQ
                && a.IsQuestion == isQ);
            }

            return Json(new {count = list.Count, animal = list[0].AnimalDescription}, JsonRequestBehavior.AllowGet);
        }

        // GET: Animals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // GET: Animals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AnimalDescription,HasQuestion,BehaviourQuestion,IsQuestion")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                db.Animals.Add(animal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(animal);
        }

        // GET: Animals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AnimalDescription,HasQuestion,BehaviourQuestion,IsQuestion")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        // GET: Animals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Animal animal = db.Animals.Find(id);
            db.Animals.Remove(animal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
