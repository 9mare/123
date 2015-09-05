using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("Reviewer")]
    public class Reviewer
    {
        [Key]
        public int ReviewerId { get; set; }

        [Display(Name = "Title")]
        public int TitleId { get; set; }

        [Display(Name = "Type")]
        public int ReviewerTypeId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string ReviewerName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Area { get; set; }

        [Required]
        public string Instituition { get; set; }

        [Display(Name = "Invitation Status")]
        public int InvitationStatusId { get; set; }

        [Display(Name = "Invitation Email Status")]
        public int InvitationEmailStatusId { get; set; }

        public int ConferenceId { get; set; }

        public bool Delete { get; set; }

        public virtual Title Title { get; set; }
        public virtual ReviewerType ReviewerType { get; set; }
        public virtual InvitationStatus InvitationStatus { get; set; }
        public virtual InvitationEmailStatus InvitationEmailStatus { get; set; }
        public virtual Conference Conference { get; set; }
    }
}