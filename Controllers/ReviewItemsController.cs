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
using System.Collections;
using System.Text;

namespace ConferenceManagementSystem.Controllers
{
    public class ReviewItemsController : Controller
    {
        private ConferenceManagementContext db = new ConferenceManagementContext();

        // GET: ReviewItems 
        public ActionResult Index()
        {
            var reviewItems = db.ReviewItems.Include(r => r.Conference);
            return View(reviewItems.ToList());
        }

        public ActionResult DisplayForm()
        {
            var reviewItems = db.ReviewItems.Include(r => r.Conference);
            //var reviewTitle = db.ReviewTitles.Include(r => r.Conference).Include(r => r.ReviewType);
            var bindModel = new ReviewViewModel();
            bindModel.ReviewItem = reviewItems.ToList();
            //bindModel.Title = reviewTitle.ToList();
            return View(bindModel);
        }

        [HttpPost]
        public ActionResult DisplayForm(List<ReviewItem> reviewItem)
        {
            var reviewItems = db.ReviewItems.Include(r => r.Conference);
            return View(reviewItems.ToList());
        }

        // GET: ReviewItems/Details/5
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

        // GET: ReviewItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReviewItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewItemId,Name,ReviewTitleId")] ReviewItem reviewItem)
        {
            try
            {
                reviewItem.ConferenceId = (int)(int)Session["sessionLoggedInUserId"];
                db.ReviewItems.Add(reviewItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(reviewItem);
            }
        }

        // GET: ReviewItems/Edit/5
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
            return View(reviewItem);
        }

        // POST: ReviewItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewItemId,Name,ReviewTitleId")] ReviewItem reviewItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviewItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reviewItem);
        }

        // GET: ReviewItems/Delete/5
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

        // POST: ReviewItems/Delete/5
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
