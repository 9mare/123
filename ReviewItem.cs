//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConferenceManagementSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReviewItem
    {
        public int ReviewItemId { get; set; }
        public int ConferenceId { get; set; }
        public string OverallComments { get; set; }
        public int AbstractSufficientlyInformative { get; set; }
        public int ClarityInThePresentationOfFindings { get; set; }
        public int MethodologyAppropriateToStudy { get; set; }
        public int ConclusionSupportedByDataAnalysis { get; set; }
        public int NoveltyOfFinding { get; set; }
    
        public virtual Conference Conference { get; set; }
    }
}