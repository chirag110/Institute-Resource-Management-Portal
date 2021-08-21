using EmptyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: User
        [Authorize(Roles = "teacher")]
        public ActionResult Index()
        {
            var inst_code = db.user_data.Where(m => m.User_Email == User.Identity.Name).FirstOrDefault();
            var res = db.user_data.Where(m => m.Inst_Code == inst_code.Inst_Code && (int)m.RegisterAs == 2).ToList();
            //ViewBag.result = res;
            return View(res);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var res = db.user_data.Where(m => m.User_Id == id).FirstOrDefault();
            return View(res);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    User std = new User();
                    var res = db.user_data.Where(m => m.User_Email == User.Identity.Name).FirstOrDefault();
                    std.Fname = collection["Fname"];
                    std.Lname = collection["Lname"];
                    std.Inst_Code = res.Inst_Code;
                    std.Inst_Name = res.Inst_Name;
                    std.Inst_Address = res.Inst_Address;
                    std.User_Email = collection["User_Email"];
                    std.Mobile = collection["Mobile"];
                    std.Passw = collection["Passw"];
                    db.user_data.Add(std);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var res = db.user_data.Where(m => m.User_Id == id).FirstOrDefault();

            return View(res);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var res = db.user_data.Where(m => m.User_Id == id).FirstOrDefault();
                User std = new User();
                std.Fname = collection["Fname"];
                std.Lname = collection["Lname"];
                std.Mobile = collection["Mobile"];
                std.User_Email = collection["User_Email"];
                std.Inst_Code = res.Inst_Code;
                std.Inst_Name = res.Inst_Name;
                std.Inst_Address = res.Inst_Address;
                std.RegisterAs = res.RegisterAs;
                std.Passw = res.Passw;
                if (ModelState.IsValid)
                {
                    db.user_data.Remove(res);
                    db.user_data.Add(std);
                    db.SaveChanges();
                }


                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var res = db.user_data.Where(m => m.User_Id == id).FirstOrDefault();
            return View(res);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var res = db.user_data.Where(m => m.User_Id == id).FirstOrDefault();
                db.user_data.Remove(res);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}