using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConferenceManagementSystem.DataAccessLayer;
using ConferenceManagementSystem.Models;

namespace ConferenceManagementSystem.Controllers
{
    public class AbstractFileFormatsController : Controller
    {
        private ConferenceManagementContext db = new ConferenceManagementContext();

        // GET: AbstractFileFormats
        public ActionResult Index(int? id)
        {
            var abstractFileFormats = db.AbstractFileFormats.Include(a => a.Alignment).Include(a => a.Conference).Include(a => a.FontName);
            return View(abstractFileFormats.ToList());
        }

        // GET: AbstractFileFormats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbstractFileFormat abstractFileFormat = db.AbstractFileFormats.Find(id);
            if (abstractFileFormat == null)
            {
                return HttpNotFound();
            }
            return View(abstractFileFormat);
        }

        // GET: AbstractFileFormats/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var abstractFileFormat = db.AbstractFileFormats.FirstOrDefault(u => u.ConferenceId == id);
            if (abstractFileFormat != null)
            {
                return HttpNotFound();
            }
            TempData["ConferenceId"] = (int)Session["sessionLoggedInUserId"];
            ViewBag.AlignmentId = new SelectList(db.Alignments, "AlignmentId", "Name");
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username");
            ViewBag.FontNameId = new SelectList(db.FontNames, "FontNameId", "Name");
            return View();
        }

        // POST: AbstractFileFormats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AbstractFileFormatId,AlignmentId,FontNameId,FontSize,Bold,Italic,Margin_Top,Margin_Bottom,Margin_Left,Margin_Right,LineSpacing,ConferenceId")] AbstractFileFormat abstractFileFormat)
        {
            try
            {
                int conferenceId = (int)Session["sessionLoggedInUserId"];
                var exist = db.AbstractFileFormats.FirstOrDefault(u => u.ConferenceId == conferenceId);
                if (exist == null)
                {
                    abstractFileFormat.ConferenceId = (int)Session["sessionLoggedInUserId"];
                    db.AbstractFileFormats.Add(abstractFileFormat);
                    db.SaveChanges();
                    return RedirectToAction("Menu", "Conference", new { id = abstractFileFormat.ConferenceId });
                }
                else
                {
                    TempData["msg"] = "<script>alert('The Paper Format already exist !!! ');</script>";
                    ViewBag.AlignmentId = new SelectList(db.Alignments, "AlignmentId", "Name", abstractFileFormat.AlignmentId);
                    ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", abstractFileFormat.ConferenceId);
                    ViewBag.FontNameId = new SelectList(db.FontNames, "FontNameId", "FontNameId", abstractFileFormat.FontNameId);
                    return View(abstractFileFormat);
                }
            }
            catch
            {
                ViewBag.AlignmentId = new SelectList(db.Alignments, "AlignmentId", "Name", abstractFileFormat.AlignmentId);
                ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", abstractFileFormat.ConferenceId);
                ViewBag.FontNameId = new SelectList(db.FontNames, "FontNameId", "FontNameId", abstractFileFormat.FontNameId);
                return View(abstractFileFormat);
            }
        }

        // GET: AbstractFileFormats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var abstractFileFormat = db.AbstractFileFormats.FirstOrDefault(u => u.ConferenceId == id);
            if (abstractFileFormat == null)
            {
                int conferenceId = id.GetValueOrDefault();
                return RedirectToAction("Create", new { id = conferenceId });
            }
            ViewBag.AlignmentId = new SelectList(db.Alignments, "AlignmentId", "Name", abstractFileFormat.AlignmentId);
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", abstractFileFormat.ConferenceId);
            ViewBag.FontNameId = new SelectList(db.FontNames, "FontNameId", "Name", abstractFileFormat.FontNameId);
            return View(abstractFileFormat);
        }

        // POST: AbstractFileFormats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AbstractFileFormatId,AlignmentId,FontNameId,FontSize,Bold,Italic,Margin_Top,Margin_Bottom,Margin_Left,Margin_Right,LineSpacing,ConferenceId")] AbstractFileFormat abstractFileFormat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abstractFileFormat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Menu", "Conference", new { id = abstractFileFormat.ConferenceId });
            }
            ViewBag.AlignmentId = new SelectList(db.Alignments, "AlignmentId", "Name", abstractFileFormat.AlignmentId);
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", abstractFileFormat.ConferenceId);
            ViewBag.FontNameId = new SelectList(db.FontNames, "FontNameId", "FontNameId", abstractFileFormat.FontNameId);
            return View(abstractFileFormat);
        }

        // GET: AbstractFileFormats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbstractFileFormat abstractFileFormat = db.AbstractFileFormats.Find(id);
            if (abstractFileFormat == null)
            {
                return HttpNotFound();
            }
            return View(abstractFileFormat);
        }

        // POST: AbstractFileFormats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AbstractFileFormat abstractFileFormat = db.AbstractFileFormats.Find(id);
            db.AbstractFileFormats.Remove(abstractFileFormat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
