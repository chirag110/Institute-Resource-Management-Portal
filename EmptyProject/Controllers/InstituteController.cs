using EmptyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyProject.Controllers
{
    [Authorize]
    public class InstituteController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Default
        [AllowAnonymous]
        public ActionResult Index()
        {
            var res = db.institutes.OrderBy(m => m.Inst_Name).ToList();
            return View(res);
        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(Institute inst)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.institutes.Add(inst);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = "+ Successfully created";
                }
                else
                {
                    ViewBag.Message = null;
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}