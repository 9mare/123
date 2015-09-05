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
    public class hfghfg : Controller
    {
        private ConferenceManagementContext db = new ConferenceManagementContext();

        // GET: hfghfg
        public ActionResult Index()
        {
            var papers = db.Papers.Include(p => p.conference).Include(p => p.topic).Include(p => p.user);
            return View(papers.ToList());
        }

        // GET: hfghfg/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paper paper = db.Papers.Find(id);
            if (paper == null)
            {
                return HttpNotFound();
            }
            return View(paper);
        }

        // GET: hfghfg/Create
        public ActionResult Create()
        {
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username");
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "Name");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email");
            return View();
        }

        // POST: hfghfg/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaperId,ConferenceId,UserId,PaperTitle,AuthorList,Co_Author,Affiliation,Presenter,Abstract,PaperDescription,AbstractFile,FullPaperFile,CameraReadyPaperFile,Keywords,TopicId,Prefix,AbstractSubmissionDate,FullPaperSubmissionDate,CameraReadyPaperSubmissionDate,AbstractSubmissionNotification,TotalNumberOfPages,Marks")] Paper paper)
        {
            if (ModelState.IsValid)
            {
                db.Papers.Add(paper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", paper.ConferenceId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "Name", paper.TopicId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", paper.UserId);
            return View(paper);
        }

        // GET: hfghfg/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paper paper = db.Papers.Find(id);
            if (paper == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", paper.ConferenceId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "Name", paper.TopicId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", paper.UserId);
            return View(paper);
        }

        // POST: hfghfg/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaperId,ConferenceId,UserId,PaperTitle,AuthorList,Co_Author,Affiliation,Presenter,Abstract,PaperDescription,AbstractFile,FullPaperFile,CameraReadyPaperFile,Keywords,TopicId,Prefix,AbstractSubmissionDate,FullPaperSubmissionDate,CameraReadyPaperSubmissionDate,AbstractSubmissionNotification,TotalNumberOfPages,Marks")] Paper paper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", paper.ConferenceId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "Name", paper.TopicId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", paper.UserId);
            return View(paper);
        }

        // GET: hfghfg/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paper paper = db.Papers.Find(id);
            if (paper == null)
            {
                return HttpNotFound();
            }
            return View(paper);
        }

        // POST: hfghfg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paper paper = db.Papers.Find(id);
            db.Papers.Remove(paper);
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
