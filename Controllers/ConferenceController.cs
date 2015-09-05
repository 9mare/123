using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ConferenceManagementSystem.Models;
using ConferenceManagementSystem.DataAccessLayer;
using System.IO;
using ConferenceManagementSystem.Filters;

namespace ConferenceManagementSystem.Controllers
{
    //[Authentication]
    public class ConferenceController : Controller
    {
        private ConferenceManagementContext db = new ConferenceManagementContext();

        // GET: /Conference/
        public ActionResult Index()
        {
            return View(db.Conferences.Where(u => u.Delete == false).ToList());
        }

        public ActionResult Menu(int? id)
        {
            Conference conference = db.Conferences.Find(id);
            Session["ConferenceId"] = conference.ConferenceId;
            return View(conference);
        }

        public ActionResult ConferenceDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conference conference = db.Conferences.Find(id);
            if (conference == null)
            {
                return HttpNotFound();
            }
            if (conference.Logo != null)
            {
                conference.PhotoString = "data:image/png;base64," + Convert.ToBase64String(conference.Logo);
            }
            conference.Password = Utilities.Function.Decrypt(conference.encryptedPassword);
            conference.ConfirmedPassword = Utilities.Function.Decrypt(conference.encryptedPassword);
            return View(conference);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ConferenceDetail([Bind(Include = "ConferenceId,LogoByte,Username,encryptedPassword,Password,ConfirmedPassword,ConferenceName,Website,Date,ContactName,Contact,PaperPrefix,LoggedIn,Logo,Short_Name,ChairmanName,ChairmanEmail,ConferencePhone,SecretariatAddress,ConferenceTime,ConferenceVenue")] Conference conference)
        {
            conference.encryptedPassword = Utilities.Function.Encrypt(conference.Password);
            if (ModelState.IsValid)
            {
                if (conference.LogoByte != null)
                {
                    if (conference.LogoByte.ContentLength > 0)
                    {
                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(conference.LogoByte.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(conference.LogoByte.ContentLength);
                        }
                        conference.Logo = imageData;
                    }
                }
                db.Entry(conference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Menu", new { id = conference.ConferenceId });
            }
            return View(conference);
        }

        public ActionResult Deadline(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var deadlines_display = db.DateDealines.Where(u => u.ConferenceId == id);

            return View();
        }

        // GET: /Conference/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conference conference = db.Conferences.Find(id);
            if (conference == null)
            {
                return HttpNotFound();
            }
            conference.DateOnly = conference.Date.Date.ToString("d");
            return View(conference);
        }

        // GET: /Conference/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Conference/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConferenceId,Username,Password,ConfirmedPassword,ConferenceName,Website,Date,ContactName,Contact,PaperPrefix,LoggedIn")] Conference conference)
        {
            if (ModelState.IsValid)
            {
                conference.encryptedPassword = Utilities.Function.Encrypt(conference.Password);
                db.Conferences.Add(conference);

                var email = new NotificationEmail();
                email.PresenterRegistration = Resources.Resource.Presenter_Registration;
                email.ParticipantRegistration = Resources.Resource.Participant_Registration;
                email.ParticipantConfirmation = Resources.Resource.Participant_Confirmation;
                email.AbstractSubmission = Resources.Resource.Abstract_Submission;
                email.AbstractAcceptance = Resources.Resource.Abstract_Acceptance;
                email.AbstractRejection = Resources.Resource.Abstract_Rejection;
                email.FullPaperSubmission = Resources.Resource.Full_Paper_Submission;
                email.PaperAcceptance = Resources.Resource.Paper_Acceptance;
                email.PaperRejection = Resources.Resource.Paper_Rejection;
                email.CameraReadyPaper = Resources.Resource.Camera_Ready_Paper;
                email.ReviewerInvitation = Resources.Resource.Reviewer_Invitation;
                email.PaperForReviewing = Resources.Resource.Paper_For_Review;
                email.FinishReview = Resources.Resource.Finish_Review;
                email.UserInvitation = Resources.Resource.User_Invitation;
                email.ConferenceId = conference.ConferenceId;
                db.NotificationEmails.Add(email);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(conference);
        }

        // GET: /Conference/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conference conference = db.Conferences.Find(id);
            if (conference == null)
            {
                return HttpNotFound();
            }
            conference.Password = Utilities.Function.Decrypt(conference.encryptedPassword);
            conference.ConfirmedPassword = Utilities.Function.Decrypt(conference.encryptedPassword);
            return View(conference);
        }

        // POST: /Conference/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConferenceId,Username,encryptedPassword,Password,ConfirmedPassword,ConferenceName,Website,Date,ContactName,Contact,PaperPrefix,LinkDirectory,LoggedIn,Logo,Short_Name,ChairmanName,ChairmanEmail,ConferencePhone,SystemEmail,SecretariatAddress,ConferenceTime,ConferenceVenue")] Conference conference)
        {
            conference.encryptedPassword = Utilities.Function.Encrypt(conference.Password);
            if (ModelState.IsValid)
            {
                db.Entry(conference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conference);
        }

        // GET: /Conference/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conference conference = db.Conferences.Find(id);
            if (conference == null)
            {
                return HttpNotFound();
            }
            conference.Password = Utilities.Function.Decrypt(conference.encryptedPassword);
            conference.ConfirmedPassword = Utilities.Function.Decrypt(conference.encryptedPassword);
            conference.encryptedPassword = null;
            return View(conference);
        }

        // POST: /Conference/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Conference conference)
        {
            conference.Delete = true;
            db.Entry(conference).State = EntityState.Modified;
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
