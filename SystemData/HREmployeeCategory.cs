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
    
    public partial class HREmployeeCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HREmployeeCategory()
        {
            this.HRDesignations = new HashSet<HRDesignation>();
        }
    
        public long Id { get; set; }
        public long IdHRCompany { get; set; }
        public string HREmployeeCategoryTitle { get; set; }
        public string HREmployeeCategoryTitleNP { get; set; }
        public string HREmployeeCategoryShortName { get; set; }
        public bool IsHigherLevelOfficer { get; set; }
        public bool IsMidLevelOfficer { get; set; }
        public bool IsLowLevelOfficer { get; set; }
        public int HREmployeeCategoryOrder { get; set; }
        public bool IsActive { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRDesignation> HRDesignations { get; set; }
        public virtual HRCompany HRCompany { get; set; }
    }
}
