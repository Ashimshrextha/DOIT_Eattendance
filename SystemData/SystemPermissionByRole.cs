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
    
    public partial class SystemPermissionByRole
    {
        public long Id { get; set; }
        public int IdRole { get; set; }
        public int IdSystemStructure { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual HREmployeeRole HREmployeeRole { get; set; }
        public virtual SystemStructure SystemStructure { get; set; }
    }
}
