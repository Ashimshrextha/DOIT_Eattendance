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
    
    public partial class MembershipProvider
    {
        public int Id { get; set; }
        public System.Guid IdLogin { get; set; }
        public Nullable<long> IdHREmployee { get; set; }
        public int IdHREmployeeRole { get; set; }
    
        public virtual HREmployee HREmployee { get; set; }
        public virtual HREmployeeRole HREmployeeRole { get; set; }
        public virtual Login Login { get; set; }
    }
}
