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
    public class RequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Discusses
        public ActionResult Index()
        {
            var Request = db.Requests.Include(d => d.Author).ToList();
            return View(Request);
        }

        // GET: Discusses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request Request = db.Requests.Find(id);
            if (Request == null)
            {
                return HttpNotFound();
            }
            return View(Request);
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
        public ActionResult Create([Bind(Include = "Id,Body,Date")] Request Request)
        {
            if (ModelState.IsValid)
            {
                Request.Author = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                //copying the note
                db.Requests.Add(Request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Request);
        }

        // GET: Discusses/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request Request = db.Requests.Include(p => p.Author).SingleOrDefault(p => p.Id == id);

            if (Request == null || Request.Author == null || (Request.Author.UserName != User.Identity.Name && !User.IsInRole("Administrators")))
            {
                return HttpNotFound();
            }
            return View(Request);
        }

        // POST: Discusses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,Date")] Request Request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Request);
        }

        // GET: Discusses/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request Request = db.Requests.Find(id);
            if (Request == null)
            {
                return HttpNotFound();
            }
            return View(Request);
        }

        // POST: Discusses/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request Request = db.Requests.Find(id);
            db.Requests.Remove(Request);
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
