//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SystemData
{
    using System;
    using System.Collections.Generic;
    
    public partial class HREmployeeAttendanceHistory
    {
        public long Id { get; set; }
        public long IdJobHistory { get; set; }
        public long IdHREmployee { get; set; }
        public System.DateTime AttendanceDate { get; set; }
        public Nullable<int> CheckInDeviceNumber { get; set; }
        public Nullable<int> CheckOutDeviceNumber { get; set; }
        public System.TimeSpan LogInTime { get; set; }
        public System.TimeSpan LogOutTime { get; set; }
        public System.TimeSpan CheckInTime { get; set; }
        public Nullable<System.TimeSpan> CheckOutTime { get; set; }
        public Nullable<System.TimeSpan> LateBy { get; set; }
        public Nullable<System.TimeSpan> CheckInEarly { get; set; }
        public Nullable<System.TimeSpan> CheckOutEarly { get; set; }
        public string LateReason { get; set; }
        public string CheckInEarlyReason { get; set; }
        public string CheckOutEarlyReason { get; set; }
        public bool IsLateReasonApproved { get; set; }
        public bool IsCheckInEarlyReasonApproved { get; set; }
        public bool IsCheckOutEarlyReasonApproved { get; set; }
        public string Remark { get; set; }
        public string ApprovedBy { get; set; }
        public int TotalWorkingInMin { get; set; }
        public Nullable<int> TotalWorkedInMin { get; set; }
        public Nullable<int> TotalOverTimeInMin { get; set; }
        public bool IsShiftCompleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual HREmployee HREmployee { get; set; }
        public virtual HREmployeeJobHistory HREmployeeJobHistory { get; set; }
    }
}