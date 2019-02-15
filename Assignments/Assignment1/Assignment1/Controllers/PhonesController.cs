using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class PhonesController : Controller
    {
        public PhonesController()
        {
            Phones = new List<PhoneBase>();

            var priv = new PhoneBase();
            priv.Id = 1;
            priv.PhoneName = "Priv";
            priv.Manufacturer = "BlackBerry";
            priv.DateReleased = new DateTime(2015, 11, 6);
            priv.MSRP = 799;
            priv.ScreenSize = 5.43;
            Phones.Add(priv);

            var galaxy = new PhoneBase
            {
                Id = 2,
                PhoneName = "Galaxy S6",
                Manufacturer = "Samsung",
                DateReleased = new DateTime(2014, 4, 10),
                MSRP = 649,
                ScreenSize = 5.1
            };
            Phones.Add(galaxy);

            Phones.Add(new PhoneBase
            {
                Id = 3,
                PhoneName = "iPhone 6s",
                Manufacturer = "Apple",
                DateReleased = new DateTime(2015, 9, 25),
                MSRP = 649,
                ScreenSize = 4.7
            });
        }

        private List<PhoneBase> Phones;

        // GET: Phones
        public ActionResult Index()
        {
            // Phones[] is re-created everytime Phones/ is accessed because
            // new phones added in Create() do not appear in the list.
            return View(Phones);
        }

        // GET: Phones/Details/5
        public ActionResult Details(int id)
        {
            return View(Phones[id - 1]);
        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            return View(new PhoneBase());
        }

        // POST: Phones/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var newItem = new PhoneBase();

                newItem.Id = Phones.Count + 1;

                newItem.PhoneName = collection["PhoneName"];
                newItem.Manufacturer = collection["Manufacturer"];

                newItem.DateReleased = Convert.ToDateTime(collection["DateReleased"]);

                int msrp;
                double ss;
                bool isNumber;

                isNumber = Int32.TryParse(collection["MSRP"], out msrp);
                newItem.MSRP = msrp;

                isNumber = double.TryParse(collection["ScreenSize"], out ss);
                newItem.ScreenSize = ss;

                Phones.Add(newItem);

                return View("Details", newItem);
            }
            catch
            {
                return View();
            }
        }

        // GET: Phones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Phones/Edit/5
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

        // GET: Phones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Phones/Delete/5
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
