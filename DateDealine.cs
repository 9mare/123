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
    
    public partial class DateDealine
    {
        public int DateDealineId { get; set; }
        public System.DateTime Abstract_Deadline { get; set; }
        public System.DateTime AbstractAcceptance_Notif { get; set; }
        public System.DateTime FullPaper_Deadline { get; set; }
        public System.DateTime PaperAcceptance_Notif { get; set; }
        public System.DateTime CameraReadyPaper_Deadline { get; set; }
        public System.DateTime EarlyBird_Deadline { get; set; }
        public System.DateTime ParticipantRegistration_Deadline { get; set; }
        public int ConferenceId { get; set; }
    
        public virtual Conference Conference { get; set; }
    }
}