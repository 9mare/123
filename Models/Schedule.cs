using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndDate { get; set; }

        public string Venue { get; set; }

        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }
    }
}