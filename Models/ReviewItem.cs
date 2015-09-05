using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{

    [Table("ReviewItem")]
    public class ReviewItem
    {
        [Key]
        public int ReviewItemId { get; set; }

        public string OverallComments { get; set; }

        public int ConferenceId { get; set; }

        public int AbstractSufficientlyInformative { get; set; }

        public int ClarityInThePresentationOfFindings { get; set; }

        public int MethodologyAppropriateToStudy { get; set; }

        public int ConclusionSupportedByDataAnalysis { get; set; }

        public int NoveltyOfFinding { get; set; }

        public virtual Conference Conference { get; set; }

    }

    public class ReviewViewModel
    {
        public List<ReviewItem> ReviewItem { get; set; }
    }
}