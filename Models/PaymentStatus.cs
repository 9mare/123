using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("PaymentStatus")]
    public class PaymentStatus
    {
        [Key]
        public int PaymentStatusId { get; set; }

        public string Status { get; set; }

        public ICollection<Attendee> Attendees { get; set; }
    }
}