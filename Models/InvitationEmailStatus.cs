using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("InvitationEmailStatus")]
    public class InvitationEmailStatus
    {
        [Key]
        public int InvitationEmailStatusId { get; set; }

        [Display(Name = "Email Status")]
        public string Status { get; set; }

        public ICollection<Reviewer> Reviewers { get; set; }
    }
}