using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment4.Controllers
{
    public class EmployeesController : Controller
    {
        Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmployeeGetAll());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            var e = m.EmployeeGetById(id.GetValueOrDefault());

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
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            var addedItem = m.EmployeeAdd(newItem);

            if (addedItem == null)
            {
                // unable to add, display user-entered fields
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", addedItem);
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            var e = m.EmployeeGetById(id.GetValueOrDefault());

            if (e == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(m.mapper.Map<EmployeeEditProfileInfoForm>(e));
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, EmployeeEditProfileInfo newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { id = newItem.EmployeeId });
            }

            if (newItem.EmployeeId != id)
            {
                // data tampering
                return RedirectToAction("index");
            }

            var editItem = m.EmployeeEditProfileInfo(newItem);

            if (editItem == null)
            {
                return RedirectToAction("edit", new { id = editItem.EmployeeId });
            }
            else
            {
                return RedirectToAction("details", new { id = editItem.EmployeeId });
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.EmployeeGetById(id.GetValueOrDefault());

            if (itemToDelete == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            // Trust the POST :id
            var result = m.EmployeeDelete(id.GetValueOrDefault());

            return RedirectToAction("index");
        }
    }
}
