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
    
    public partial class NotificationEmail
    {
        public int EmailId { get; set; }
        public string PresenterRegistration { get; set; }
        public string ParticipantRegistration { get; set; }
        public string ParticipantConfirmation { get; set; }
        public string AbstractSubmission { get; set; }
        public string AbstractAcceptance { get; set; }
        public string AbstractRejection { get; set; }
        public string FullPaperSubmission { get; set; }
        public string PaperAcceptance { get; set; }
        public string PaperRejection { get; set; }
        public string CameraReadyPaper { get; set; }
        public string ReviewerInvitation { get; set; }
        public string PaperForReviewing { get; set; }
        public string FinishReview { get; set; }
        public string UserInvitation { get; set; }
        public int ConferenceId { get; set; }
    
        public virtual Conference Conference { get; set; }
    }
}
