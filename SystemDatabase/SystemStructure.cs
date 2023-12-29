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
    
    public partial class SystemStructure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SystemStructure()
        {
            this.SystemPermissionByHREmployee = new HashSet<SystemPermissionByHREmployee>();
            this.SystemPermissionByRole = new HashSet<SystemPermissionByRole>();
        }
    
        public int Id { get; set; }
        public int IdSystemMenu { get; set; }
        public int IdParent_SystemStructure { get; set; }
        public string Header { get; set; }
        public string Controller { get; set; }
        public string ActionName { get; set; }
        public string CSSClass { get; set; }
        public string CSSClass1 { get; set; }
        public string CSSIcon1 { get; set; }
        public string CSSIcon { get; set; }
        public int ParentOrder { get; set; }
        public Nullable<int> ChildOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsTabbed { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public bool IsMenu { get; set; }
    
        public virtual SystemMenu SystemMenu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SystemPermissionByHREmployee> SystemPermissionByHREmployee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SystemPermissionByRole> SystemPermissionByRole { get; set; }
    }
}
