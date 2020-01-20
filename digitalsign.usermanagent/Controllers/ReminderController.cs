using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace digitalsign.usermanagent.Controllers
{
    public class ReminderController : Controller
    {
        // GET: Reminder
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reminder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reminder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reminder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Reminder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reminder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Reminder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reminder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}