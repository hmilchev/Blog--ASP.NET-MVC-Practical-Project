using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog__ASP.NET_MVC_Practical_Project.Models;

namespace Blog__ASP.NET_MVC_Practical_Project.Controllers
{
    public class OfftopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Discusses
        public ActionResult Index()
        {
            var offtopic = db.Offtopics.Include(d => d.Author).ToList();
            return View(offtopic);
        }

        // GET: Discusses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offtopic offtopic = db.Offtopics.Find(id);
            if (offtopic == null)
            {
                return HttpNotFound();
            }
            return View(offtopic);
        }

        // GET: Discusses/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Discusses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Body,Date")] Offtopic offtopic)
        {
            if (ModelState.IsValid)
            {
                offtopic.Author = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                //copying the note
                db.Offtopics.Add(offtopic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offtopic);
        }

        // GET: Discusses/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offtopic offtopic = db.Offtopics.Include(p => p.Author).SingleOrDefault(p => p.Id == id);

            if (offtopic == null || offtopic.Author == null || (offtopic.Author.UserName != User.Identity.Name && !User.IsInRole("Administrators")))
            {
                return HttpNotFound();
            }
            return View(offtopic);
        }

        // POST: Discusses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,Date")] Offtopic offtopic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offtopic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offtopic);
        }

        // GET: Discusses/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offtopic offtopic = db.Offtopics.Find(id);
            if (offtopic == null)
            {
                return HttpNotFound();
            }
            return View(offtopic);
        }

        // POST: Discusses/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offtopic offtopic = db.Offtopics.Find(id);
            db.Offtopics.Remove(offtopic);
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
