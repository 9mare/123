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
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;

namespace ConferenceManagementSystem.Controllers
{
    public class PaperController : Controller
    {
        private ConferenceManagementContext db = new ConferenceManagementContext();

        public ActionResult PaperDetail(int? id)
        {
            int conferenceId = id.GetValueOrDefault();
            var papers = db.Papers.Include(p => p.conference).Include(p => p.topic).Include(p => p.user);
            return View(papers.Where(p => p.ConferenceId == conferenceId));
        }

        // GET: /Paper/
        public ActionResult Index(int? id)
        {
            TempData["ConferenceId"] = (int)Session["ConferenceId"];
            int conferenceId = id.GetValueOrDefault();
            int userId = (int)Session["sessionLoggedInUserId"];
            if (db.Papers.FirstOrDefault(u => u.ConferenceId == conferenceId && u.UserId == userId) == null)
            {
                return RedirectToAction("Create", new { id = conferenceId });
            }
            var papers = db.Papers.Include(p => p.conference).Include(p => p.topic).Include(p => p.user);
            return View(papers.Where(p => p.ConferenceId == conferenceId && p.UserId == id).ToList());
        }

        // GET: /Paper/Details/5
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

        public FileResult DownloadFile(string fileName , string paperPrefix)
        {
            var paths = "~/Paper/" + paperPrefix + "/";
            var path = Path.Combine(Server.MapPath(paths), fileName);
            return File(path, MimeMapping.GetMimeMapping(path), fileName);
        }

        // GET: /Paper/Create
        public ActionResult Create(int? id)
        {
            int conferenceId = id.GetValueOrDefault();
            int userId = (int)Session["sessionLoggedInUserId"];
            TempData["ConferenceId"] = conferenceId;
            var dateline = db.DateDealines.FirstOrDefault(u => u.ConferenceId == conferenceId);
            if (dateline.Abstract_Deadline != null && dateline.Abstract_Deadline < DateTime.Now.Date)
            {
                TempData["msg"] = "<script>alert('Abstract Paper Submission Is Closed');</script>";
                return RedirectToAction("Home", "Main", new { id = conferenceId });
            }
            else
            {
                ViewBag.conferenceName = Session["ConferenceName"].ToString();
                ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username");
                ViewBag.TopicId = new SelectList(db.Topics.Where(u => u.Delete == false && u.ConferenceId == conferenceId), "TopicId", "Name");
                ViewBag.UserId = new SelectList(db.Users, "UserId", "Email");
                return View();
            }
        }

        // POST: /Paper/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include= "PaperTitle,AuthorList,Affiliation,Presenter,AbstractFile,Abstract,Keywords,TopicId,TotalNumberOfPages")] Paper paper)
        {
            try
            {
                int userId = (int)Session["sessionLoggedInUserId"];
                var submission = new Paper();
                submission.PaperTitle = paper.PaperTitle;
                submission.AuthorList = paper.AuthorList;
                submission.Affiliation = paper.Affiliation;
                submission.Presenter = paper.Presenter;
                submission.Abstract = paper.Abstract;
                submission.Keywords = paper.Keywords;
                submission.TopicId = paper.TopicId;
                submission.UserId = userId;
                submission.AbstractTotalNumberOfPages = paper.AbstractTotalNumberOfPages;
                submission.ConferenceId = (int)Session["ConferenceId"];
                Conference conference = db.Conferences.FirstOrDefault(u => u.ConferenceId == submission.ConferenceId);
                AbstractFileFormat fileformat = db.AbstractFileFormats.FirstOrDefault(u => u.ConferenceId == submission.ConferenceId);
                Paper papers = db.Papers.FirstOrDefault(u => u.ConferenceId == submission.ConferenceId);
                if(papers == null)
                {
                    submission.Prefix = conference.PaperPrefix + 1;
                }
                else
                {
                    Paper last = db.Papers.OrderByDescending(u => u.PaperId).FirstOrDefault(u => u.ConferenceId == submission.ConferenceId);
                    string[] word = last.Prefix.Split('-');
                    int paperNumber = Int32.Parse(word[1].ToString()) + 1;
                    submission.Prefix = conference.PaperPrefix + paperNumber;
                }
                string filePath = FileUrl(paper.AbstractFile, submission.Prefix);
                string wordFilePath = WordFileUrl(paper.AbstractFile, submission.Prefix);
                if (fileformat != null)
                {
                    Document doc = new Document();
                    doc.LoadFromFile(wordFilePath);
                    submission.AbstractTotalNumberOfPages = doc.PageCount;
                    int count = 0;
                    int styleName = 0;

                    foreach (Section section in doc.Sections)
                    {
                        foreach (Paragraph paragraph in section.Paragraphs)
                        {
                            paragraph.Format.LineSpacing = (float)fileformat.LineSpacing;
                            string text = paragraph.Text.ToLower();
                            if (text.Length > 0 && text.Equals("abstract"))
                            {
                                count = 1;
                            }
                            if (text.Length > 1 && count == 1)
                            {
                                ParagraphStyle style = new ParagraphStyle(doc);
                                paragraph.Format.HorizontalAlignment = HorizontalAlignment.Left;
                                style.Name = styleName.ToString();
                                style.CharacterFormat.FontName = fileformat.FontName.Name;
                                style.CharacterFormat.FontSize = fileformat.FontSize;
                                doc.Styles.Add(style);
                                paragraph.ApplyStyle(style.Name);
                            }
                            styleName++;
                        }
                        section.PageSetup.Margins.Top = (float)fileformat.Margin_Top;
                        section.PageSetup.Margins.Bottom = (float)fileformat.Margin_Bottom;
                        section.PageSetup.Margins.Left = (float)fileformat.Margin_Left;
                        section.PageSetup.Margins.Right = (float)fileformat.Margin_Right;
                    }
                    doc.SaveToFile(wordFilePath);
                }
                submission.AbstractSubmissionDate = DateTime.Now.ToString();
                submission.AbstractFile = filePath;
                db.Papers.Add(submission);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = submission.UserId });
            }
            catch
            {
                ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username");
                ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "Name");
                ViewBag.UserId = new SelectList(db.Users, "UserId", "Email");
                return View(paper);
            }
        }

        // GET: /Paper/Edit/5
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

        // POST: /Paper/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "PaperId,ConferenceId,UserId,PaperTitle,AuthorList,Co_Author,Affiliation,Presenter,Abstract,PaperDescription,AbstractFile,FullPaperFile,CameraReadyPaperFile,Keywords,TopicId,Prefix,AbstractSubmissionDate,FullPaperSubmissionDate,CameraReadyPaperSubmissionDate,AbstractSubmissionNotification,TotalNumberOfPages,Marks")] Paper paper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = paper.ConferenceId });
            }
            ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", paper.ConferenceId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "Name", paper.TopicId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", paper.UserId);
            return View(paper);
        }

        public ActionResult FullPaperIndex(int? id)
        {
            TempData["ConferenceId"] = (int)Session["ConferenceId"];
            int conferenceId = id.GetValueOrDefault();
            int userId = (int)Session["sessionLoggedInUserId"];
            var dateline = db.DateDealines.FirstOrDefault(u => u.ConferenceId == conferenceId);
            var exist = db.Papers.FirstOrDefault(u => u.ConferenceId == conferenceId && u.UserId == userId);
            if (exist != null)
            {
                if (exist.FullPaperFile == null)
                {
                    if (dateline.FullPaper_Deadline < DateTime.Now)
                    {
                        return RedirectToAction("FullPaperSubmission", new { id = exist.PaperId });
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Full Paper Submission Is Closed');</script>";
                        return RedirectToAction("Home", "Main", new { id = conferenceId });
                    }
                }
                var papers = db.Papers.Include(p => p.conference).Include(p => p.topic).Include(p => p.user);
                return View(papers.Where(p => p.ConferenceId == conferenceId && p.UserId == id && p.FullPaperFile != null).ToList());
            }
            else
            {
                TempData["msg"] = "<script>alert('Please Submit Abstract Paper');</script>";
                return RedirectToAction("Home", "Main", new { id = conferenceId });
            }
        }

        public ActionResult FullPaperSubmission(int? id)
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

        // POST: /Paper/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult FullPaperSubmission([Bind(Include = "PaperId,ConferenceId,UserId,PaperTitle,AuthorList,Co_Author,Affiliation,Presenter,Abstract,PaperDescription,AbstractFile,FullPaperFile,CameraReadyPaperFile,Keywords,TopicId,Prefix,AbstractSubmissionDate,FullPaperSubmissionDate,CameraReadyPaperSubmissionDate,AbstractSubmissionNotification,TotalNumberOfPages,Marks")] Paper paper)
        {
            try
            {
                Conference conference = db.Conferences.FirstOrDefault(u => u.ConferenceId == paper.ConferenceId);
                AbstractFileFormat fileformat = db.AbstractFileFormats.FirstOrDefault(u => u.ConferenceId == paper.ConferenceId);
                string filePath = FileUrl(paper.FullPaperFile, paper.Prefix);
                string wordFilePath = WordFileUrl(paper.FullPaperFile, paper.Prefix);

                if (fileformat != null)
                {
                    Document doc = new Document();
                    doc.LoadFromFile(wordFilePath);
                    int count = 0;
                    int styleName = 0;

                    foreach (Section section in doc.Sections)
                    {
                        foreach (Paragraph paragraph in section.Paragraphs)
                        {
                            paragraph.Format.LineSpacing = (float)fileformat.LineSpacing;
                            string text = paragraph.Text.ToLower();
                            if (text.Length > 0 && text.Equals("abstract"))
                            {
                                count = 1;
                            }
                            if (text.Length > 1 && count == 1)
                            {
                                ParagraphStyle style = new ParagraphStyle(doc);
                                paragraph.Format.HorizontalAlignment = HorizontalAlignment.Left;
                                style.Name = styleName.ToString();
                                style.CharacterFormat.FontName = fileformat.FontName.Name;
                                style.CharacterFormat.FontSize = fileformat.FontSize;
                                doc.Styles.Add(style);
                                paragraph.ApplyStyle(style.Name);
                            }
                            styleName++;
                        }
                        section.PageSetup.Margins.Top = (float)fileformat.Margin_Top;
                        section.PageSetup.Margins.Bottom = (float)fileformat.Margin_Bottom;
                        section.PageSetup.Margins.Left = (float)fileformat.Margin_Left;
                        section.PageSetup.Margins.Right = (float)fileformat.Margin_Right;
                    }
                    doc.SaveToFile(wordFilePath);
                }
                paper.FullPaperSubmissionDate = DateTime.Now.ToString();
                paper.FullPaperFile = filePath;
                if (ModelState.IsValid)
                {
                    db.Entry(paper).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("FullPaperIndex", new { id = paper.ConferenceId });
            }
            catch
            {
                ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", paper.ConferenceId);
                ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "Name", paper.TopicId);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", paper.UserId);
                return View(paper);
            }
        }

        public ActionResult CameraReadyIndex(int? id)
        {
            TempData["ConferenceId"] = (int)Session["ConferenceId"];
            int conferenceId = id.GetValueOrDefault();
            int userId = (int)Session["sessionLoggedInUserId"];
            var dateline = db.DateDealines.FirstOrDefault(u => u.ConferenceId == conferenceId);
            var exist = db.Papers.FirstOrDefault(u => u.ConferenceId == conferenceId && u.UserId == userId);
            if (exist != null)
            {
                if (exist.FullPaperFile == null)
                {
                    if (dateline.FullPaper_Deadline < DateTime.Now)
                    {
                        return RedirectToAction("CameraReadySubmission", new { id = exist.PaperId });
                    }
                    else
                    {
                            TempData["msg"] = "<script>alert('Full Paper Submission Is Closed');</script>";
                            return RedirectToAction("Home", "Main", new { id = conferenceId });
                    }
                }
                var papers = db.Papers.Include(p => p.conference).Include(p => p.topic).Include(p => p.user);
                return View(papers.Where(p => p.ConferenceId == conferenceId && p.UserId == id && p.CameraReadyPaperFile != null).ToList());
            }
            else
            {
                TempData["msg"] = "<script>alert('Please Submit Full Paper');</script>";
                return RedirectToAction("Home", "Main", new { id = conferenceId });
            }

        }

        public ActionResult CameraReadySubmission(int? id)
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

        // POST: /Paper/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CameraReadySubmission([Bind(Include = "PaperId,ConferenceId,UserId,PaperTitle,AuthorList,Co_Author,Affiliation,Presenter,Abstract,PaperDescription,AbstractFile,FullPaperFile,CameraReadyPaperFile,Keywords,TopicId,Prefix,AbstractSubmissionDate,FullPaperSubmissionDate,CameraReadyPaperSubmissionDate,AbstractSubmissionNotification,TotalNumberOfPages,Marks")] Paper paper)
        {
            try
            {
                Conference conference = db.Conferences.FirstOrDefault(u => u.ConferenceId == paper.ConferenceId);
                AbstractFileFormat fileformat = db.AbstractFileFormats.FirstOrDefault(u => u.ConferenceId == paper.ConferenceId);
                string filePath = FileUrl(paper.CameraReadyPaperFile, paper.Prefix);
                string wordFilePath = WordFileUrl(paper.CameraReadyPaperFile, paper.Prefix);

                if (fileformat != null)
                {
                    Document doc = new Document();
                    doc.LoadFromFile(wordFilePath);
                    int count = 0;
                    int styleName = 0;

                    foreach (Section section in doc.Sections)
                    {
                        foreach (Paragraph paragraph in section.Paragraphs)
                        {
                            paragraph.Format.LineSpacing = (float)fileformat.LineSpacing;
                            string text = paragraph.Text.ToLower();
                            if (text.Length > 0 && text.Equals("abstract"))
                            {
                                count = 1;
                            }
                            if (text.Length > 1 && count == 1)
                            {
                                ParagraphStyle style = new ParagraphStyle(doc);
                                paragraph.Format.HorizontalAlignment = HorizontalAlignment.Left;
                                style.Name = styleName.ToString();
                                style.CharacterFormat.FontName = fileformat.FontName.Name;
                                style.CharacterFormat.FontSize = fileformat.FontSize;
                                doc.Styles.Add(style);
                                paragraph.ApplyStyle(style.Name);
                            }
                            styleName++;
                        }
                        section.PageSetup.Margins.Top = (float)fileformat.Margin_Top;
                        section.PageSetup.Margins.Bottom = (float)fileformat.Margin_Bottom;
                        section.PageSetup.Margins.Left = (float)fileformat.Margin_Left;
                        section.PageSetup.Margins.Right = (float)fileformat.Margin_Right;
                    }
                    doc.SaveToFile(wordFilePath);
                }
                paper.CameraReadyPaperSubmissionDate = DateTime.Now.ToString();
                paper.CameraReadyPaperFile = filePath;
                if (ModelState.IsValid)
                {
                    db.Entry(paper).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("CameraReadyIndex", new { id = paper.ConferenceId });
            }
            catch
            {
                ViewBag.ConferenceId = new SelectList(db.Conferences, "ConferenceId", "Username", paper.ConferenceId);
                ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "Name", paper.TopicId);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", paper.UserId);
                return View(paper);
            }
        }

        private string FileUrl(string filePath, string paperprefix)
        {
            string wordFilePath = filePath;
            var path1 = "";
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var paths = "~/Paper/" + paperprefix + "/";
                    System.IO.Directory.CreateDirectory(Server.MapPath(paths));
                    var path = Path.Combine(Server.MapPath(paths), fileName);

                    wordFilePath = HttpUtility.UrlPathEncode(paths + fileName);
                    file.SaveAs(path);
                    path1 = path;
                }
            }

            return path1;
        }

        private string WordFileUrl(string filePath, string paperprefix)
        {
            string path = filePath;

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var paths = "~/Paper/" + paperprefix + "/";
                    path = Path.Combine(Server.MapPath(paths), fileName);

                }
            }

            return path;
        }

        // GET: /Paper/Delete/5
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

        // POST: /Paper/Delete/5
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
