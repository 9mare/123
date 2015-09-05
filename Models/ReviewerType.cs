using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("ReviewerType")]
    public class ReviewerType
    {
        [Key]
        public int ReviewerTypeId { get; set; }

        [Display(Name = "Reviewer")]
        public string Name { get; set; }

        public ICollection<Reviewer> Reviewers { get; set; }
    }
}