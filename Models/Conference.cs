using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("Conference")]
    public class Conference
    {
        [Key]
        public int ConferenceId { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d+)[a-zA-Z0-9]{8,15}$", ErrorMessage = "Username must be 8-15 characters, and include letters and numbers.")]
        public string Username { get; set; }

        public string encryptedPassword { get; set; }

        [NotMapped]
        [Required]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d+)[a-zA-Z0-9]{8,}$", ErrorMessage = "Password must be at least 8 characters, and include letters and numbers.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password fields did not match")]
        public string ConfirmedPassword { get; set; }

        [Required]
        [Display(Name = "Conference Name")]
        public string ConferenceName { get; set; }

        [Required]
        [Display(Name = "Conference Website")]
        [RegularExpression(@"(http(s)?://)?([\w-]+\.)+[\w-]+[.com]+(/[/?%&=]*)?", ErrorMessage = "The website url must be valid")]
        public string Website { get; set; }

        [Display(Name = "Conference Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMMM/yyyy}")]
        public DateTime Date { get; set; }

        [NotMapped]
        public string DateOnly { get; set; }

        [Required]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Required]
        [Display(Name = "Contactsl")]
        public string Contact { get; set; }

        [Required]
        [Display(Name = "Paper Prefix")]
        public string PaperPrefix { get; set; }

        public bool LoggedIn { get; set; }

        public byte[] Logo { get; set; }

        [NotMapped]
        public HttpPostedFileBase LogoByte { get; set; }

        [NotMapped]
        public string PhotoString { get; set; }

        [Display(Name = "Conference Short Name")]
        public string Short_Name { get; set; }

        [Display(Name = "Chairman Name")]
        public string ChairmanName { get; set; }

        [Display(Name = "Chairman Email")]
        [EmailAddress(ErrorMessage = "This email address is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string ChairmanEmail { get; set; }

        [Display(Name = "Conference Phone")]
        public string ConferencePhone { get; set; }

        [Display(Name = "Secretariat Address")]
        public string SecretariatAddress { get; set; }

        [Display(Name = "Conference Time")]
        public string ConferenceTime { get; set; }

        [Display(Name = "Conference Fax")]
        public string Fax { get; set; }

        [Display(Name = "Conference Venue")]
        public string ConferenceVenue { get; set; }

        public bool Delete { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<Fee> Fees { get; set; }
        public virtual ICollection<DateDealine> DateDealines { get; set; }

        [Display(Name = "Notification Emails")]
        public virtual ICollection<NotificationEmail> NotificationEmails { get; set; }

        public ICollection<Attendee> Attendees { get; set; }
        public virtual ICollection<Paper> Papers { get; set; }
        public virtual ICollection<Reviewer> Reviewers { get; set; }

        [Display(Name = "Review Items")]
        public virtual ICollection<ReviewItem> ReviewItems { get; set; }

        [Display(Name = "Abstract File Formats")]
        public virtual ICollection<AbstractFileFormat> AbstractFileFormats { get; set; }
    }
}