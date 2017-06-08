using _01_Client.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_Client.Controllers
{
    public class TasksController : Controller
    {
       
        TasksData db = new TasksData("Provider=sqloledb;Data Source=SQL6001,1433;Initial Catalog=DB_A1F08F_ITPortalRep;User Id=DB_A1F08F_ITPortalRep_admin;Password=VladVsemRad123456;");
        // GET: Tasks
        public ActionResult Index()
        {
           
            string json = db.ResultToJson(db.SelectQuery("Users", db.SelectMany("Name", "Surname", "Title")));
            return Json(json,JsonRequestBehavior.AllowGet);
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tasks/Edit/5
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

        // GET: Tasks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tasks/Delete/5
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
