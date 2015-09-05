using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("Topic")]
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }

        [Required]
        public string Name { get; set; }

        public int ConferenceId { get; set; }

        public bool Delete { get; set; }

        public virtual Conference conference { get; set; }

        public virtual ICollection<Paper> Papers { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }

    }
}