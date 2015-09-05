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
    public class DeadlineController : Controller
    {
        private ConferenceManagementContext db = new ConferenceManagementContext();

        // GET: /Deadline/
        public ActionResult Index()
        {
            var datedealines = db.DateDealines.Include(d => d.conference);
            return View(datedealines.ToList());
        }

        // GET: /Deadline/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateDealine datedealine = db.DateDealines.Find(id);
            if (datedealine == null)
            {
                return HttpNotFound();
            }
            return View(datedealine);
        }

        // GET: /Deadline/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var datedealine = db.DateDealines.FirstOrDefault(u => u.ConferenceId == id);
            if (datedealine != null)
            {
                return HttpNotFound();
            }
            TempData["ConferenceId"] = (int)Session["sessionLoggedInUserId"];
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username");
            return View();
        }

        // POST: /Deadline/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DateDealineId,Abstract_Deadline,AbstractAcceptance_Notif,FullPaper_Deadline,PaperAcceptance_Notif,CameraReadyPaper_Deadline,EarlyBird_Deadline,ParticipantRegistration_Deadline,ConferenceId")] DateDealine datedealine)
        {
            try
            {
                int conferenceId = (int)Session["sessionLoggedInUserId"];
                var exist = db.DateDealines.FirstOrDefault(u => u.ConferenceId == conferenceId);
                if (exist == null)
                {
                    if (datedealine.AbstractAcceptance_Notif >= datedealine.Abstract_Deadline)
                    {
                        if (datedealine.FullPaper_Deadline >= datedealine.AbstractAcceptance_Notif)
                        {
                            if (datedealine.PaperAcceptance_Notif >= datedealine.FullPaper_Deadline)
                            {
                                if (datedealine.CameraReadyPaper_Deadline >= datedealine.PaperAcceptance_Notif)
                                {
                                    if (datedealine.ParticipantRegistration_Deadline >= datedealine.EarlyBird_Deadline)
                                    {
                                        datedealine.ConferenceId = (int)Session["sessionLoggedInUserId"];
                                        db.DateDealines.Add(datedealine);
                                        db.SaveChanges();
                                        return RedirectToAction("Menu", "Conference", new { id = datedealine.ConferenceId });
                                    }
                                    else
                                    {
                                        TempData["msg"] = "<script>alert('Participant Registration Deadline must be after Early Bird Deadline ');</script>";
                                        ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                                        return View(datedealine);
                                    }
                                }
                                else
                                {
                                    TempData["msg"] = "<script>alert('CameraReady Paper Deadline must be after Paper Acceptance Notification ');</script>";
                                    ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                                    return View(datedealine);
                                }
                            }
                            else
                            {
                                TempData["msg"] = "<script>alert('Paper Acceptance must be after Full Paper Deadline ');</script>";
                                ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                                return View(datedealine);
                            }
                        }
                        else
                        {
                            TempData["msg"] = "<script>alert('Full Paper Deadline must be after Abstract Acceptance Notification ');</script>";
                            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                            return View(datedealine);
                        }
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Abstract Acceptance Notification must be after Abstract Deadline ');</script>";
                        ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                        return View(datedealine);
                    }
                }
                else
                {
                    TempData["msg"] = "<script>alert('The Paper Format already exist !!! ');</script>";
                    ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                    return View(datedealine);
                }
            }
            catch
            {
                ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                return View(datedealine);
            }
        }

        // GET: /Deadline/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int conferenceId = id.GetValueOrDefault();
            DateDealine datedealine = db.DateDealines.FirstOrDefault(u => u.ConferenceId == conferenceId);
            if (datedealine == null)
            {
                return RedirectToAction("Create", new { id = conferenceId });
            }
            datedealine = db.DateDealines.FirstOrDefault(u => u.ConferenceId == conferenceId);
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
            return View(datedealine);
        }

        // POST: /Deadline/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DateDealineId,Abstract_Deadline,AbstractAcceptance_Notif,FullPaper_Deadline,PaperAcceptance_Notif,CameraReadyPaper_Deadline,EarlyBird_Deadline,ParticipantRegistration_Deadline,ConferenceId")] DateDealine datedealine)
        {
            try
            {
                if (datedealine.AbstractAcceptance_Notif >= datedealine.Abstract_Deadline)
                {
                    if (datedealine.FullPaper_Deadline >= datedealine.AbstractAcceptance_Notif)
                    {
                        if (datedealine.PaperAcceptance_Notif >= datedealine.FullPaper_Deadline)
                        {
                            if (datedealine.CameraReadyPaper_Deadline >= datedealine.PaperAcceptance_Notif)
                            {
                                if (datedealine.ParticipantRegistration_Deadline >= datedealine.EarlyBird_Deadline)
                                {
                                    db.Entry(datedealine).State = EntityState.Modified;
                                    db.SaveChanges();
                                    return RedirectToAction("Menu", "Conference", new { id = datedealine.ConferenceId });
                                }
                                else
                                {
                                    TempData["msg"] = "<script>alert('Participant Registration Deadline must be after Early Bird Deadline ');</script>";
                                    ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                                    return View(datedealine);
                                }
                            }
                            else
                            {
                                TempData["msg"] = "<script>alert('CameraReady Paper Deadline must be after Paper Acceptance Notification ');</script>";
                                ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                                return View(datedealine);
                            }
                        }
                        else
                        {
                            TempData["msg"] = "<script>alert('Paper Acceptance must be after Full Paper Deadline ');</script>";
                            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                            return View(datedealine);
                        }
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Full Paper Deadline must be after Abstract Acceptance Notification ');</script>";
                        ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                        return View(datedealine);
                    }
                }
                else
                {
                    TempData["msg"] = "<script>alert('Abstract Acceptance Notification must be after Abstract Deadline ');</script>";
                    ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                    return View(datedealine);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", datedealine.ConferenceId);
                return View(datedealine);
            }
        }

        // GET: /Deadline/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateDealine datedealine = db.DateDealines.Find(id);
            if (datedealine == null)
            {
                return HttpNotFound();
            }
            return View(datedealine);
        }

        // POST: /Deadline/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DateDealine datedealine = db.DateDealines.Find(id);
            db.DateDealines.Remove(datedealine);
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
