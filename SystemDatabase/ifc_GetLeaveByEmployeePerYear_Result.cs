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
    
    public partial class ifc_GetLeaveByEmployeePerYear_Result
    {
        public Nullable<long> IdHREmployee { get; set; }
        public Nullable<long> IdLeaveType { get; set; }
        public string LeaveTitle { get; set; }
        public Nullable<bool> IsIncludePublicHoliday { get; set; }
        public Nullable<bool> IsIncludeWeekend { get; set; }
        public Nullable<int> OccuringTime { get; set; }
        public Nullable<decimal> TotalLeave { get; set; }
        public Nullable<int> EligibleDays { get; set; }
        public Nullable<bool> IsSaving { get; set; }
        public Nullable<int> YearNP { get; set; }
    }
}
