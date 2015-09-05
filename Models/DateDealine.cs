using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("DateDealine")]
    public class DateDealine
    {
        [Key]
        public int DateDealineId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMMM/yyyy}")]
        [Display(Name = "Abstract Deadline")]
        public DateTime Abstract_Deadline{ get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMMM/yyyy}")]
        [Display(Name = "Abstract Acceptance Notification")]
        public DateTime AbstractAcceptance_Notif { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMMM/yyyy}")]
        [Display(Name = "FullPaper Deadline")]
        public DateTime FullPaper_Deadline { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMMM/yyyy}")]
        [Display(Name = "Paper Acceptance Notification")]
        public DateTime PaperAcceptance_Notif { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMMM/yyyy}")]
        [Display(Name = "Camer-Ready Paper Deadline")]
        public DateTime CameraReadyPaper_Deadline { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMMM/yyyy}")]
        [Display(Name = "Early Bird Deadline")]
        public DateTime EarlyBird_Deadline { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMMM/yyyy}")]
        [Display(Name = "Participant Registration Deadline")]
        public DateTime ParticipantRegistration_Deadline { get; set; }

        public int ConferenceId { get; set; }

        public virtual Conference conference { get; set; }
    }
}