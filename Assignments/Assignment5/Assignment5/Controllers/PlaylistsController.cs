using Assignment5.Data.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class PlaylistsController : Controller
    {
        private ChinookDataManager m = new ChinookDataManager();

        // GET: Playlists
        public ActionResult Index()
        {
            return View(m.PlaylistGetAll());
        }

        // GET: Playlists/Details/5
        public ActionResult Details(int? id)
        {
            return View(m.PlaylistGetByIdWithTracks(id.GetValueOrDefault()));
        }

        // GET: Playlists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Playlists/Create
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

        // GET: Playlists/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Playlists/Edit/5
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

        // GET: Playlists/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Playlists/Delete/5
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
