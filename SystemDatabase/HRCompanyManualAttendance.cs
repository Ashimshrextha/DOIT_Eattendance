//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SystemDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class HRCompanyManualAttendance
    {
        public long Id { get; set; }
        public long IdHrCompany { get; set; }
        public Nullable<long> IdApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedOn { get; set; }
        public string ApprovedBy { get; set; }
        public string Document { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> IdAttendanceStatus { get; set; }
        public string FolderName { get; set; }
        public string Remark { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] ContentData { get; set; }
        public Nullable<System.DateTime> PunchDate { get; set; }
        public Nullable<System.TimeSpan> PunchTime { get; set; }
        public Nullable<long> IdHREmployee { get; set; }
    
        public virtual HRCompany HRCompany { get; set; }
        public virtual HRCompanyLeaveStatusType HRCompanyLeaveStatusType { get; set; }
        public virtual HREmployee HREmployee { get; set; }
        public virtual HREmployee HREmployee1 { get; set; }
    }
}
