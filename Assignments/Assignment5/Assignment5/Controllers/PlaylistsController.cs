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
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.PlaylistGetByIdWithTracks(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form, based on the fetched matching object
                //var form = m._mapper.Map<PlaylistEditTracksForm>(o);
                var form = new PlaylistEditTracksForm();
                form.Id = o.Id;
                form.Name = o.Name;

                // For the multi select list, configure the "selected" items
                // Notice the use of the Select() method, 
                // which allows us to select/return/use only some properties from the source
                var selectedValues = o.PlaylistTracks.Select(pt => pt.TrackId);

                // For clarity, use the named parameter feature of C#
                form.TrackList = new MultiSelectList
                    (items: m.TrackGetAll(),
                    dataValueField: "Id",
                    dataTextField: "Name",
                    selectedValues: selectedValues);

                return View(form);
            }
        }

        // POST: Playlists/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTracks editPlaylist)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit", new { id = editPlaylist.Id });
            }

            if (id.GetValueOrDefault() != editPlaylist.Id)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("Index");
            }

            // Attempt to do the update
            var editedPlaylist = m.PlaylistEditTracks(editPlaylist);

            if (editedPlaylist == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit", new { id = editPlaylist.Id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("Details", new { id = editPlaylist.Id });
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
