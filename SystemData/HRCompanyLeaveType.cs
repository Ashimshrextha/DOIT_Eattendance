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
    
    public partial class HRCompanyLeaveType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRCompanyLeaveType()
        {
            this.HREmployeeLeaveHistories = new HashSet<HREmployeeLeaveHistory>();
        }
    
        public long Id { get; set; }
        public long IdHRCompany { get; set; }
        public string HRCompanyLeaveTitle { get; set; }
        public string HRCompanyLeaveTitleNP { get; set; }
        public string HRCompanyLeaveShotName { get; set; }
        public string HRCompanyLeaveShotNameNP { get; set; }
        public bool IsIncludeWeekend { get; set; }
        public bool IsIncludePublicHoliday { get; set; }
        public bool IsEligiblePermanentStaff { get; set; }
        public bool IsSaving { get; set; }
        public int TotalLeaveDay { get; set; }
        public int LimitDay { get; set; }
        public bool IsForGeneral { get; set; }
        public bool IsForMale { get; set; }
        public bool IsForFemale { get; set; }
        public bool IsReoccuring { get; set; }
        public bool IsActive { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HREmployeeLeaveHistory> HREmployeeLeaveHistories { get; set; }
        public virtual HRCompany HRCompany { get; set; }
    }
}
