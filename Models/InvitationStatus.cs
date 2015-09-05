using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("InvitationStatus")]
    public class InvitationStatus
    {
        [Key]
        public int InvitationStatusId { get; set; }

        [Display(Name = "Invitation Status")]
        public string Status { get; set; }

        public ICollection<Reviewer> Reviewers { get; set; }
    }
}