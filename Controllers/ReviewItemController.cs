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
    public class ReviewItemController : Controller
    {
        private ConferenceManagementContext db = new ConferenceManagementContext();

        // GET: ReviewItem
        public ActionResult Index()
        {
            var reviewItems = db.ReviewItems.Include(r => r.Conference);
            return View(reviewItems.ToList());
        }

        // GET: ReviewItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewItem reviewItem = db.ReviewItems.Find(id);
            if (reviewItem == null)
            {
                return HttpNotFound();
            }
            return View(reviewItem);
        }

        public ActionResult Sample()
        {
            TempData["ConferenceId"] = (int)Session["ConferenceId"];
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username");
            return View();
        }

        // GET: ReviewItem/Create
        public ActionResult Create()
        {
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username");
            return View();
        }

        // POST: ReviewItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewItemId,OverallComments,ConferenceId,AbstractSufficientlyInformative,ClarityInThePresentationOfFindings,MethodologyAppropriateToStudy,ConclusionSupportedByDataAnalysis,NoveltyOfFinding")] ReviewItem reviewItem)
        {
            if (ModelState.IsValid)
            {
                db.ReviewItems.Add(reviewItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", reviewItem.ConferenceId);
            return View(reviewItem);
        }

        // GET: ReviewItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewItem reviewItem = db.ReviewItems.Find(id);
            if (reviewItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", reviewItem.ConferenceId);
            return View(reviewItem);
        }

        // POST: ReviewItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewItemId,OverallComments,ConferenceId,AbstractSufficientlyInformative,ClarityInThePresentationOfFindings,MethodologyAppropriateToStudy,ConclusionSupportedByDataAnalysis,NoveltyOfFinding")] ReviewItem reviewItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviewItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", reviewItem.ConferenceId);
            return View(reviewItem);
        }

        // GET: ReviewItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewItem reviewItem = db.ReviewItems.Find(id);
            if (reviewItem == null)
            {
                return HttpNotFound();
            }
            return View(reviewItem);
        }

        // POST: ReviewItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReviewItem reviewItem = db.ReviewItems.Find(id);
            db.ReviewItems.Remove(reviewItem);
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
