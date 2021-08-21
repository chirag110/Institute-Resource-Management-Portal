using EmptyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var res = db.user_data.Where(m => m.User_Email == User.Identity.Name).FirstOrDefault();
            return View(res);
        }
    }
}