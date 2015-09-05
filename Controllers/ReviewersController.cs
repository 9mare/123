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
using System.Net.Mail;
using System.Text;
using ConferenceManagementSystem.Filters;

namespace ConferenceManagementSystem.Controllers
{
    [Authentication]
    public class ReviewersController : Controller
    {
        private ConferenceManagementContext db = new ConferenceManagementContext();

        // GET: Reviewers
        public ActionResult Index(int? id)
        {
            int conferenceId = id.GetValueOrDefault();
            TempData["ConferenceId"] = conferenceId;
            var reviewers = db.Reviewers.Include(r => r.InvitationEmailStatus).Include(r => r.InvitationStatus).Include(r => r.ReviewerType).Include(r => r.Title).Include(r => r.Conference).Where(r => r.ConferenceId == id);
            return View(reviewers.Where(u => u.Delete == false).ToList());
        }

        // GET: Reviewers/Details/5
        public ActionResult Send(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int reviewerId = id.GetValueOrDefault();
            Reviewer reviewer = db.Reviewers.FirstOrDefault(u => u.ReviewerId == reviewerId);
            if (reviewer == null)
            {
                return HttpNotFound();
            }
            int conferenceId = (int)Session["sessionLoggedInUserId"];
            var emailMessage = db.NotificationEmails.FirstOrDefault(u => u.ConferenceId == conferenceId);
            var conference = db.Conferences.FirstOrDefault(u => u.ConferenceId == conferenceId);
            var builder = new StringBuilder();
            builder.AppendLine(emailMessage.ReviewerInvitation);
            MailAddress fromMessage = new MailAddress("danlong@hotmail.com",conference.ConferenceName);
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = fromMessage;
            message.To.Add(new MailAddress(reviewer.Email));
            message.Subject = "UTAR Conference Manager Reviewer Invitation";
            message.Body = builder.ToString();
            SmtpClient client = new SmtpClient();
            client.Send(message);
            reviewer.Delete = false;
            reviewer.InvitationEmailStatusId = 2;
            db.Entry(reviewer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Reviewers", new { id = reviewer.ConferenceId });
        }

        // GET: Reviewers/Create
        public ActionResult Create(int? id)
        {
            int conferenceId = id.GetValueOrDefault();
            TempData["ConferenceId"] = conferenceId;
            ViewBag.InvitationEmailStatusId = new SelectList(db.InvitationEmailStatuses, "InvitationEmailStatusId", "InvitationEmailStatusId");
            ViewBag.InvitationStatusId = new SelectList(db.InvitationStatuses, "InvitationStatusId", "Status");
            ViewBag.ReviewerTypeId = new SelectList(db.ReviewerTypes, "ReviewerTypeId", "Name");
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "Name");
            return View();
        }

        // POST: Reviewers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewerId,TitleId,ReviewerTypeId,ReviewerName,Email,Area,Instituition,InvitationStatusId,InvitationEmailStatusId")] Reviewer reviewer)
        {
            try
            {
                reviewer.ConferenceId = (int)Session["sessionLoggedInUserId"];
                reviewer.InvitationEmailStatusId = 1;
                reviewer.InvitationStatusId = 1;
                db.Reviewers.Add(reviewer);
                db.SaveChanges();
                return RedirectToAction("Index", "Reviewers", new { id = reviewer.ConferenceId });
            }
            catch (Exception ex)
            {
                
                ViewBag.InvitationEmailStatusId = new SelectList(db.InvitationEmailStatuses, "InvitationEmailStatusId", "InvitationEmailStatusId", reviewer.InvitationEmailStatusId);
                ViewBag.InvitationStatusId = new SelectList(db.InvitationStatuses, "InvitationStatusId", "Status", reviewer.InvitationStatusId);
                ViewBag.ReviewerTypeId = new SelectList(db.ReviewerTypes, "ReviewerTypeId", "Name", reviewer.ReviewerTypeId);
                ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "Name", reviewer.TitleId);
                return View(reviewer);
            }
        }

        // GET: Reviewers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviewer reviewer = db.Reviewers.Find(id);
            if (reviewer == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvitationEmailStatusId = new SelectList(db.InvitationEmailStatuses, "InvitationEmailStatusId", "InvitationEmailStatusId", reviewer.InvitationEmailStatusId);
            ViewBag.InvitationStatusId = new SelectList(db.InvitationStatuses, "InvitationStatusId", "Status", reviewer.InvitationStatusId);
            ViewBag.ReviewerTypeId = new SelectList(db.ReviewerTypes, "ReviewerTypeId", "Name", reviewer.ReviewerTypeId);
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "Name", reviewer.TitleId);
            return View(reviewer);
        }

        // POST: Reviewers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewerId,TitleId,ReviewerTypeId,ReviewerName,Email,Area,Instituition,InvitationStatusId,InvitationEmailStatusId,ConferenceId")] Reviewer reviewer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviewer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = reviewer.ConferenceId });
            }
            ViewBag.InvitationEmailStatusId = new SelectList(db.InvitationEmailStatuses, "InvitationEmailStatusId", "InvitationEmailStatusId", reviewer.InvitationEmailStatusId);
            ViewBag.InvitationStatusId = new SelectList(db.InvitationStatuses, "InvitationStatusId", "Status", reviewer.InvitationStatusId);
            ViewBag.ReviewerTypeId = new SelectList(db.ReviewerTypes, "ReviewerTypeId", "Name", reviewer.ReviewerTypeId);
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "Name", reviewer.TitleId);
            return View(reviewer);
        }

        // GET: Reviewers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviewer reviewer = db.Reviewers.Find(id);
            if (reviewer == null)
            {
                return HttpNotFound();
            }
            return View(reviewer);
        }

        // POST: Reviewers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Reviewer reviewer)
        {
            reviewer.Delete = true;
            db.Entry(reviewer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = reviewer.ConferenceId });
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
