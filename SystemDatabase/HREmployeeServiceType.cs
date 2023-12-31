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
    
    public partial class HREmployeeServiceType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HREmployeeServiceType()
        {
            this.HREmployeeServiceTypeGroup = new HashSet<HREmployeeServiceTypeGroup>();
            this.HREmployeeServiceTypeGroup1 = new HashSet<HREmployeeServiceTypeGroup>();
            this.HREmployeeJobHistories = new HashSet<HREmployeeJobHistory>();
        }
    
        public long Id { get; set; }
        public long IdHRCompany { get; set; }
        public string HREmployeeServiceTypeTitle { get; set; }
        public string HREmployeeServiceTypeShortName { get; set; }
        public int HREmployeeServiceTypeOrder { get; set; }
        public bool IsActive { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public bool IsPermanentServiceType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HREmployeeServiceTypeGroup> HREmployeeServiceTypeGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HREmployeeServiceTypeGroup> HREmployeeServiceTypeGroup1 { get; set; }
        public virtual HRCompany HRCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HREmployeeJobHistory> HREmployeeJobHistories { get; set; }
    }
}
