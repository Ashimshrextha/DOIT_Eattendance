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
    
    public partial class HREmployeeRelationType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HREmployeeRelationType()
        {
            this.HREmployees = new HashSet<HREmployee>();
        }
    
        public int Id { get; set; }
        public string RelationType { get; set; }
        public string RelationTypeNP { get; set; }
        public bool IsActive { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HREmployee> HREmployees { get; set; }
    }
}
