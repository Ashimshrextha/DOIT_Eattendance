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
    
    public partial class proc_GetHREmployeeLeaveHistoryPerYearDateReport_Result
    {
        public Nullable<long> IdLeaveType { get; set; }
        public Nullable<int> IdMasterLeaveTitle { get; set; }
        public string LeaveTitle { get; set; }
        public Nullable<decimal> DeductedLeaveDay { get; set; }
        public Nullable<decimal> AvailableLeave { get; set; }
        public Nullable<decimal> TotalTakenLeave { get; set; }
        public Nullable<decimal> RemainingLeave { get; set; }
        public Nullable<int> TotalFestival { get; set; }
        public Nullable<int> TotalWeekend { get; set; }
        public Nullable<decimal> TotalHalfDays { get; set; }
        public Nullable<int> TotalRequestDays { get; set; }
        public Nullable<int> EligiblePerYear { get; set; }
        public Nullable<decimal> SavingLeave { get; set; }
    }
}
