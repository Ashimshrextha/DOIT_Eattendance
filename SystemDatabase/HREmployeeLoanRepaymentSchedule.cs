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
    
    public partial class HREmployeeLoanRepaymentSchedule
    {
        public System.Guid Id { get; set; }
        public System.Guid IdHREmployeeLoan { get; set; }
        public long IdHREmployee { get; set; }
        public System.DateTime ScheduleDate { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal BalanceToBePaid { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual HREmployee HREmployee { get; set; }
        public virtual HREmployeeLoan HREmployeeLoan { get; set; }
    }
}