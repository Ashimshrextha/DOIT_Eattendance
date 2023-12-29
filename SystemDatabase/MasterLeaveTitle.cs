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
    
    public partial class MasterLeaveTitle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MasterLeaveTitle()
        {
            this.HRCompanyLeaveType = new HashSet<HRCompanyLeaveType>();
            this.HREmployeeLeaveBalance = new HashSet<HREmployeeLeaveBalance>();
            this.HREmployeeLeaveHistory = new HashSet<HREmployeeLeaveHistory>();
            this.HREmployeeLeaveManagement = new HashSet<HREmployeeLeaveManagement>();
        }
    
        public int Id { get; set; }
        public string LeaveTitle { get; set; }
        public string LeaveTitleNP { get; set; }
        public string LeaveShortTitle { get; set; }
        public string ColorCode { get; set; }
        public bool IsActive { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRCompanyLeaveType> HRCompanyLeaveType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HREmployeeLeaveBalance> HREmployeeLeaveBalance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HREmployeeLeaveHistory> HREmployeeLeaveHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HREmployeeLeaveManagement> HREmployeeLeaveManagement { get; set; }
    }
}
