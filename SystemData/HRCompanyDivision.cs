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
    
    public partial class HRCompanyDivision
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRCompanyDivision()
        {
            this.HREmployeeJobHistories = new HashSet<HREmployeeJobHistory>();
        }
    
        public long Id { get; set; }
        public long IdHRCompany { get; set; }
        public long IdDivisionType { get; set; }
        public Nullable<long> IdParent_HRCompanyDivision { get; set; }
        public string HRCompanyDivisionName { get; set; }
        public string HRCompanyDivisionNameNP { get; set; }
        public string HRCompanyDivisionShortName { get; set; }
        public bool IsActive { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual HRCompanyDivisionType HRCompanyDivisionType { get; set; }
        public virtual HRCompany HRCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HREmployeeJobHistory> HREmployeeJobHistories { get; set; }
    }
}