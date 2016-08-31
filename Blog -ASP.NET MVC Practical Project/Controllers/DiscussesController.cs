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
    public class DiscussesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Discusses
        public ActionResult Index()
        {
            var discuss = db.Discusses.Include(d => d.Author).ToList();
            return View(discuss);
        }

        // GET: Discusses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discuss discuss = db.Discusses.Find(id);
            if (discuss == null)
            {
                return HttpNotFound();
            }
            return View(discuss);
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
        public ActionResult Create([Bind(Include = "Id,Body,Date")] Discuss discuss)
        {
            if (ModelState.IsValid)
            {
                discuss.Author = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                //copying the note
                db.Discusses.Add(discuss);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discuss);
        }

        // GET: Discusses/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discuss discuss = db.Discusses.Include(p => p.Author).SingleOrDefault(p => p.Id == id);

            if (discuss == null || discuss.Author == null || (discuss.Author.UserName != User.Identity.Name && !User.IsInRole("Administrators")))
            {
                return HttpNotFound();
            }
            return View(discuss);
        }

        // POST: Discusses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,Date")] Discuss discuss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discuss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discuss);
        }

        // GET: Discusses/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discuss discuss = db.Discusses.Find(id);
            if (discuss == null)
            {
                return HttpNotFound();
            }
            return View(discuss);
        }

        // POST: Discusses/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discuss discuss = db.Discusses.Find(id);
            db.Discusses.Remove(discuss);
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
