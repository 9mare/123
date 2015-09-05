using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("Attendee")]
    public class Attendee
    {
        [Key]
        public int AttendeeId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMMM/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime RegDate { get; set; }

        public int PaymentStatusId { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public int PaymentAmount { get; set; }

        public string PaymentDetail { get; set; }

        public string ReceiptNumber { get; set; }

        public int ConferenceId { get; set; }

        [Display(Name = "Registration Fee Category")]
        public int FeeId { get; set; }

        public int UserId { get; set; }

        [Display(Name="Register Me As")]
        public int UserTypeId { get; set; }

        public bool Delete { get; set; }

        public virtual Conference conference { get; set; }
        public virtual Fee fee { get; set; }
        public virtual User user { get; set; }
        public virtual UserType usertype { get; set; }
        public virtual PaymentStatus paymentStatus { get; set; }
    }

    public class ParticipantViewModel
    {
        public ICollection<User> Users { get; set; }

        public Attendee Attendees { get; set; }

    }
}