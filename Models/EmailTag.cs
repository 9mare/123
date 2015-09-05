using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("EmailTag")]
    public class EmailTag
    {
        [Key]
        public int EmailTagsId { get; set; }

        public string Tags { get; set; }

        public string Representation { get; set; }

    }
}