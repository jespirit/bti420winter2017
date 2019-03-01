using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class TracksController : Controller
    {
        Manager m = new Manager();

        // GET: Tracks
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        public ActionResult Sorted()
        {
            return View("index", m.TrackGetAllSorted());
        }

        public ActionResult Pop()
        {
            return View("index", m.TrackGetAllPop());
        }

        public ActionResult DeepPurple()
        {
            return View("index", m.TrackGetAllDeepPurple());
        }

        public ActionResult Longest()
        {
            return View("index", m.TrackGetAllTop100Longest());
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            var t = m.TrackGetByIdWithDetail(id.GetValueOrDefault());

            if (t == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(t);
            }
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {
            // Create a form
            var form = new TrackAddForm();

            // Configure the SelectList for the item-selection element on the HTML Form
            form.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title");
            form.GenreList = new SelectList(m.GenreGetAll(), "GenreId", "Name");
            form.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name");

            return View(form);
        }

        // POST: Tracks/Create
        [HttpPost]
        public ActionResult Create(TrackAdd newTrack)
        {
            if (!ModelState.IsValid)
            {
                return View(newTrack);
            }

            var addedTrack = m.TrackAdd(newTrack);

            if (addedTrack == null)
            {
                return View(newTrack);
            }
            else
            {
                return RedirectToAction("Details", new { id = addedTrack.TrackId });
            }
        }

        // GET: Tracks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tracks/Edit/5
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

        // GET: Tracks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tracks/Delete/5
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
