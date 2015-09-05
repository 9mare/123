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
using ConferenceManagementSystem.Filters;

namespace ConferenceManagementSystem.Controllers
{
    [Authentication]
    public class FeeController : Controller
    {
        private ConferenceManagementContext db = new ConferenceManagementContext();

        // GET: /Fee/
        public ActionResult Index(int? id)
        {
            int conferenceId = id.GetValueOrDefault();
            Session["ConferenceId"] = conferenceId;
            var fees = db.Fees.Include(f => f.conference).Where(f => f.ConferenceId == id);
            TempData["ConferenceId"] = conferenceId;
            return View(fees.Where(u => u.Delete == false).ToList());
        }

        // GET: /Fee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // GET: /Fee/Create
        public ActionResult Create(int? id)
        {
            int conferenceId = id.GetValueOrDefault();
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username");
            TempData["ConferenceId"] = conferenceId;
            return View();
        }

        // POST: /Fee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="FeeId,Category,Currency,EarlyBird,Normal,ConferenceId")] Fee fee)
        {
            try
            {
                fee.Delete = false;
                fee.ConferenceId = (int)Session["ConferenceId"];
                fee.DisplayForNormal = fee.Category + " (" + fee.Currency + " " + fee.Normal + ")";
                fee.DisplayForEarlyBird = fee.Category + " (" + fee.Currency + " " + fee.Normal + ")";
                db.Fees.Add(fee);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = fee.ConferenceId});
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error! " + ex.Message);
                ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", fee.ConferenceId);
                return View(fee);
            }
        }

        // GET: /Fee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", fee.ConferenceId);
            return View(fee);
        }

        // POST: /Fee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="FeeId,Category,Currency,EarlyBird,Normal,ConferenceId")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = fee.ConferenceId });
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", fee.ConferenceId);
            return View(fee);
        }

        // GET: /Fee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // POST: /Fee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Fee fee)
        {
            fee.Delete = true;
            db.Entry(fee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = fee.ConferenceId });
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
