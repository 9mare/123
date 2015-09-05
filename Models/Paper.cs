using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("Paper")]
    public class Paper
    {
        [Key]
        public int PaperId { get; set; }

        public int ConferenceId { get; set; }
        public virtual Conference conference { get; set; }

        public int UserId { get; set; }
        public virtual User user { get; set; }

        [Required]
        public string PaperTitle { get; set; }

        [Required]
        public string AuthorList { get; set; }

        [Required]
        public string Affiliation { get; set; }

        [Required]
        public string Presenter { get; set; }

        [Required]
        public string Abstract { get; set; }

        public string PaperDescription { get; set; }

        public string AbstractFile { get; set; }

        public string FullPaperFile { get; set; }

        public string CameraReadyPaperFile { get; set; }

        [Required]
        public string Keywords { get; set; }

        public int TopicId { get; set; }

        public string Prefix { get; set; }

        public virtual Topic topic { get; set; }

        public string AbstractSubmissionDate { get; set; }

        public string FullPaperSubmissionDate { get; set; }

        public string CameraReadyPaperSubmissionDate { get; set; }

        public int AbstractPaperStatus { get; set; }

        public int FullPaperStatus { get; set; }

        public int AbstractTotalNumberOfPages { get; set; }

        public int FullPaperTotalNumberOfPages { get; set; }
    
    }
}