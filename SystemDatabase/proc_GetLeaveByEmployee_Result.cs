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
    
    public partial class proc_GetLeaveByEmployee_Result
    {
        public long IdLeaveType { get; set; }
        public string LeaveTitle { get; set; }
        public string LeaveShortTitle { get; set; }
        public int TotalLeaveDay { get; set; }
        public int SavingLimitDay { get; set; }
        public bool IsSaving { get; set; }
        public int AdditionalDays { get; set; }
        public bool EnableHalfDay { get; set; }
        public bool IsContinueLeave { get; set; }
        public int ApprovalDeadLineDays { get; set; }
        public int DocUploadDeadLineDays { get; set; }
        public bool IsDocumentUpload { get; set; }
        public int OccuringTime { get; set; }
        public bool IsApprovalByHRCompanyHead { get; set; }
    }
}
