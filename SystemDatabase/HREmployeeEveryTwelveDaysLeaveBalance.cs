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
    
    public partial class HREmployeeEveryTwelveDaysLeaveBalance
    {
        public int Id { get; set; }
        public Nullable<long> IdHREmployee { get; set; }
        public Nullable<System.DateTime> JoingDate { get; set; }
        public Nullable<int> LeaveYear { get; set; }
        public Nullable<decimal> TotalLeave { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual HREmployee HREmployee { get; set; }
    }
}
