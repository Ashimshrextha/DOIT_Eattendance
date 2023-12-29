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
    
    public partial class HREmployeeSalaryHistory
    {
        public long Id { get; set; }
        public long IdHREmployee { get; set; }
        public System.DateTime SalaryDate { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal OverTimeAllowance { get; set; }
        public int TotalLeaveTakenDay { get; set; }
        public int TotalPaidLeaveDay { get; set; }
        public decimal LeaveDayRatePerDay { get; set; }
        public int TotalWorkingDay { get; set; }
        public int TotalWorkedDay { get; set; }
        public int TotalWorkingInMinute { get; set; }
        public int TotalWorkedInMinute { get; set; }
        public int TotalOverTimeInMinute { get; set; }
        public decimal OverTimeRatePerMinute { get; set; }
        public decimal TADA { get; set; }
        public decimal TotalAdvance { get; set; }
        public decimal DeducatedAdvance { get; set; }
        public decimal BalanceAdvance { get; set; }
        public decimal GrandTotalSalary { get; set; }
        public decimal ReceivalableSalary { get; set; }
        public decimal ReceivedSalary { get; set; }
        public bool IsSalaryReceived { get; set; }
        public string SalaryReceivedBy { get; set; }
        public Nullable<System.DateTime> SalaryReceivedDate { get; set; }
        public string SalaryPaidVia { get; set; }
        public string BankAccountNumber { get; set; }
        public Nullable<decimal> CITFund { get; set; }
        public Nullable<decimal> InsuranceAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> VatAmount { get; set; }
        public string Remark { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual HREmployee HREmployee { get; set; }
    }
}