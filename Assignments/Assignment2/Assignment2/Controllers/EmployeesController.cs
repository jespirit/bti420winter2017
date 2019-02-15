using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    public class EmployeesController : Controller
    {
        private Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmployeeGetAll());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            var e = m.EmployeeGetById(id);

            if (e == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(e);
            }
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeeAdd newItem)
        {
            // Validate input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process
            var addedItem = m.EmployeeAddNew(newItem);

            if (addedItem == null)
            {
                // Something went wrong, show form again
                return View(addedItem);
            }
            else
            {
                // Returns HTTP 302 with Location header value set to the destination resource
                return RedirectToAction("Details", new { id = addedItem.EmployeeId });
            }
            
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employees/Edit/5
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

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
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
