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
using System.Net.Mail;
using System.Text;
using ConferenceManagementSystem.Filters;

namespace ConferenceManagementSystem.Controllers
{
    //[Authentication]
    public class AttendeeController : Controller
    {
        private ConferenceManagementContext db = new ConferenceManagementContext();

        // GET: /Attendee/
        public ActionResult Index(int? id)
        {
            int conferenceId = id.GetValueOrDefault();
            var attendees = db.Attendees.Include(a => a.conference).Include(a => a.fee).Include(a => a.user).Include(a => a.usertype).Where(u => u.ConferenceId == conferenceId);
            TempData["ConferenceId"] = conferenceId;
            return View(attendees.Where(u => u.Delete == false).ToList());
        }

        public ActionResult Status(int? id)
        {
            int userId = (int)Session["sessionLoggedInUserId"];
            int conferenceId = id.GetValueOrDefault();
            TempData["ConferenceId"] = conferenceId;
            var attendee = db.Attendees.FirstOrDefault(u => u.ConferenceId == conferenceId && u.UserId == userId);
            if (attendee == null)
            {
                return RedirectToAction("Create", new { id = conferenceId });
            }
            var attendees = db.Attendees.Include(a => a.conference).Include(a => a.fee).Include(a => a.user).Include(a => a.usertype).Where(u => u.UserId == userId && u.ConferenceId == id);
            return View(attendees.ToList());
        }

        public ActionResult UserDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendee attendee = db.Attendees.FirstOrDefault(u => u.AttendeeId == id);
            if (attendee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", attendee.ConferenceId);
            ViewBag.FeeId = new SelectList(db.Fees, "FeeId", "Category", attendee.FeeId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "Status", attendee.PaymentStatusId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", attendee.UserId);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Name", attendee.UserTypeId);
            return View(attendee);
        }

        // POST: asdsadsad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserDetail([Bind(Include = "AttendeeId,RegDate,PaymentStatusId,PaymentAmount,PaymentDetail,ReceiptNumber,ConferenceId,FeeId,UserId,UserTypeId,Delete")] Attendee attendee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = attendee.ConferenceId });
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", attendee.ConferenceId);
            ViewBag.FeeId = new SelectList(db.Fees, "FeeId", "Category", attendee.FeeId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "Status", attendee.PaymentStatusId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", attendee.UserId);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Name", attendee.UserTypeId);
            return View(attendee);
        }

        // GET: /Attendee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendee attendee = db.Attendees.Find(id);
            if (attendee == null)
            {
                return HttpNotFound();
            }
            return View(attendee);
        }

        // GET: /Attendee/Create
        public ActionResult Create(int? id)
        {
            int conferenceId = id.GetValueOrDefault();
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username");
            ViewBag.FeeId = new SelectList(db.Fees.Where(u => u.Delete == false && u.ConferenceId == conferenceId), "FeeId", "DisplayForNormal");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email");
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Name");
            TempData["UserId"] = (int)Session["sessionLoggedInUserId"];
            TempData["ConferenceId"] = conferenceId;
            return View();
        }

        // POST: /Attendee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AttendeeId,ConferenceId,FeeId,UserId,UserTypeId")] Attendee attendee)
        {
            if (ModelState.IsValid)
            {
                var conference = db.Conferences.FirstOrDefault(u => u.ConferenceId == attendee.ConferenceId);
                var user = db.Users.FirstOrDefault(u => u.UserId == attendee.UserId);
                var emailMessage = db.NotificationEmails.FirstOrDefault(u => u.ConferenceId == attendee.ConferenceId);
                var builder = new StringBuilder();

                if (emailMessage.ParticipantRegistration != null && emailMessage.PresenterRegistration != null)
                {
                    if (attendee.UserTypeId == 1)
                    {
                        emailMessage.ParticipantRegistration = emailMessage.ParticipantRegistration.Replace("{USER_TITLE}",user.Title.Name);
                        //emailMessage.ParticipantRegistration = Utilities.Function.EmailConvertion(emailMessage.ParticipantRegistration);
                        builder.AppendLine(emailMessage.ParticipantRegistration.ToString());
                    }
                    else
                    {
                        builder.AppendLine(emailMessage.PresenterRegistration.ToString());
                    }
                    var message = new MailMessage();
                    message.IsBodyHtml = true;
                    message.From = new MailAddress("danlong@hotmail.com", conference.ConferenceName);
                    message.To.Add(new MailAddress(user.Email));
                    message.Subject = "Registration at" + conference.ConferenceName;
                    message.Body = builder.ToString();
                    SmtpClient client = new SmtpClient();
                    client.Send(message);
                }
                attendee.PaymentStatusId = 1;
                attendee.RegDate = DateTime.Now;
                db.Attendees.Add(attendee);
                db.SaveChanges();
                return RedirectToAction("Home", "Main", new { id = attendee.ConferenceId });

            }

            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", attendee.ConferenceId);
            ViewBag.FeeId = new SelectList(db.Fees.Where(u => u.Delete == false && u.ConferenceId == attendee.ConferenceId), "FeeId", "DisplayForNormal");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", attendee.UserId);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Name", attendee.UserTypeId);
            return View(attendee);
        }

        // GET: /Attendee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int conferenceId = (int)Session["ConferenceId"];
            int attendeeId = id.GetValueOrDefault();
            Attendee attendee = db.Attendees.FirstOrDefault(u => u.AttendeeId == attendeeId );
            if (attendee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", attendee.ConferenceId);
            ViewBag.FeeId = new SelectList(db.Fees.Where(u => u.Delete == false && u.ConferenceId == conferenceId), "FeeId", "DisplayForNormal", attendee.FeeId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", attendee.UserId);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Name", attendee.UserTypeId);
            TempData["ConferenceId"] = Session["sessionLoggedInUserId"];
            return View(attendee);
        }

        // POST: /Attendee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "AttendeeId,ConferenceId,RegDate,PaymentStatusId,FeeId,UserId,UserTypeId")] Attendee attendee)
        {
            int conferenceId = (int)Session["ConferenceId"];
            if (ModelState.IsValid)
            {
                var conference = db.Conferences.FirstOrDefault(u => u.ConferenceId == attendee.ConferenceId);
                var user = db.Users.FirstOrDefault(u => u.UserId == attendee.UserId);
                var emailMessage = db.NotificationEmails.FirstOrDefault(u => u.ConferenceId == attendee.ConferenceId);
                var dateline = db.DateDealines.FirstOrDefault(u => u.ConferenceId == attendee.ConferenceId);
                var builder = new StringBuilder();
                if (emailMessage.ParticipantRegistration != null && emailMessage.PresenterRegistration != null)
                {
                    if (attendee.UserTypeId == 1)
                    {
                        emailMessage.ParticipantRegistration = emailMessage.ParticipantRegistration.Replace("{USER_TITLE}", user.Title.Name)
                            .Replace("{USER_NAME}", user.FullName).Replace("{CONF_NAME}", conference.ConferenceName)
                            .Replace("{CONF_SHORT}", conference.Short_Name).Replace("{FULL_PAPER_DATE}", dateline.FullPaper_Deadline.ToString())
                            .Replace("{CONF_TIME}", conference.ConferenceTime).Replace("{CONF_VENUE}", conference.ConferenceVenue);
                        builder.AppendLine(emailMessage.ParticipantRegistration.ToString());
                    }
                    else
                    {
                        emailMessage.ParticipantRegistration = emailMessage.ParticipantRegistration.Replace("{USER_TITLE}", user.Title.Name)
                            .Replace("{USER_GIVENNAME}", user.FullName).Replace("{CONF_NAME}", conference.ConferenceName)
                            .Replace("{CONF_SHORT}", conference.Short_Name).Replace("{CONF_TELP}", conference.ConferencePhone)
                            .Replace("{CONF_FAX}", conference.Fax).Replace("{CONF_EMAIL}", conference.ChairmanEmail);
                        builder.AppendLine(emailMessage.PresenterRegistration.ToString());
                    }
                    var message = new MailMessage();
                    message.IsBodyHtml = true;
                    message.From = new MailAddress("danlong@hotmail.com", conference.ConferenceName);
                    message.To.Add(new MailAddress(user.Email));
                    message.Subject = "Registration at" + conference.ConferenceName;
                    message.Body = builder.ToString();
                    SmtpClient client = new SmtpClient();
                    client.Send(message);
                }
                db.Entry(attendee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Status", new { id = attendee.ConferenceId });
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", attendee.ConferenceId);
            ViewBag.FeeId = new SelectList(db.Fees.Where(u => u.Delete == false && u.ConferenceId == conferenceId), "FeeId", "DisplayForNormal");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", attendee.UserId);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Name", attendee.UserTypeId);
            return View(attendee);
        }

        // GET: /Attendee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendee attendee = db.Attendees.FirstOrDefault(u => u.AttendeeId == id);
            if (attendee == null)
            {
                return HttpNotFound();
            }
            return View(attendee);
        }

        // POST: /Attendee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Attendee attendee)
        {
            attendee.Delete = true;
            db.Entry(attendee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = attendee.ConferenceId });
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
