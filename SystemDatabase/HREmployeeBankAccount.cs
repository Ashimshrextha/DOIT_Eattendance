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
    
    public partial class HREmployeeBankAccount
    {
        public long Id { get; set; }
        public long IdHREmployee { get; set; }
        public long IdBank { get; set; }
        public string NomineeName { get; set; }
        public string BankAccountNumber { get; set; }
        public string AccountType { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual HREmployee HREmployee { get; set; }
        public virtual HREmployeeBankList HREmployeeBankList { get; set; }
        public virtual HREmployeeBankList HREmployeeBankList1 { get; set; }
    }
}
