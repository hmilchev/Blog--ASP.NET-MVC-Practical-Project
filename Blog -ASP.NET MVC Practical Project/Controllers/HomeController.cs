using Blog__ASP.NET_MVC_Practical_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Blog__ASP.NET_MVC_Practical_Project.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var latestsPosts = db.Posts.Include(p => p.Author).OrderByDescending(p => p.Date).Take(4);
            return View(latestsPosts);
        }
    }
}