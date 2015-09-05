using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("Fee")]
    public class Fee
    {
        [Key]
        public int FeeId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Early Bird")]
        [RegularExpression(@"\d+[a-zA-Z0-9](\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public decimal EarlyBird { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Normal")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public decimal Normal { get; set; }

        public string DisplayForNormal { get; set; }

        public string DisplayForEarlyBird { get; set; }

        public bool Delete { get; set; }

        public int ConferenceId { get; set; }
        public virtual Conference conference { get; set; }

        public ICollection<Attendee> Attendees { get; set; }
    }
}