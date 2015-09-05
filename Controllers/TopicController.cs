using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConferenceManagementSystem.Models;
using ConferenceManagementSystem.DataAccessLayer;

namespace ConferenceManagementSystem.Controllers
{
    public class TopicController : Controller
    {
        private ConferenceManagementContext db = new ConferenceManagementContext();

        // GET: /Topic/
        public ActionResult Index(int? id)
        {
            int conferenceId = id.GetValueOrDefault();
            var topics = db.Topics.Include(t => t.conference).Where(t => t.ConferenceId == conferenceId);
            TempData["ConferenceId"] = conferenceId;
            return View(topics.Where(u => u.Delete == false).ToList());
        }

        // GET: /Topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: /Topic/Create
        public ActionResult Create(int? id)
        {
            int conferenceId2 = id.GetValueOrDefault();
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username");
            TempData["ConferenceId"] = conferenceId2;
            return View();
        }

        // POST: /Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicId,Name,ConferenceId")] Topic topic)
        {
            try
            {
                topic.Delete = false;
                topic.ConferenceId = (int)Session["sessionLoggedInUserId"];
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = topic.ConferenceId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error! " + ex.Message);
                ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", topic.ConferenceId);
                return View(topic);
            }
        }

        // GET: /Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", topic.ConferenceId);
            return View(topic);
        }

        // POST: /Topic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicId,Name,ConferenceId")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = topic.ConferenceId });
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", topic.ConferenceId);
            return View(topic);
        }

        // GET: /Topic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: /Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Topic topic)
        {
            topic.Delete = true;
            db.Entry(topic).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = topic.ConferenceId });
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
