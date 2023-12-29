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
    
    public partial class HREmployeeAddress
    {
        public long Id { get; set; }
        public long IdHREmployee { get; set; }
        public long IdPermanentCity { get; set; }
        public long IdPermanentDistrict { get; set; }
        public long IdTemporaryDistrict { get; set; }
        public long IdTemporaryCity { get; set; }
        public string PermanentWardNumber { get; set; }
        public string TemporaryWardNumber { get; set; }
        public string Remark { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public string TerminalIp { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual City City { get; set; }
        public virtual City City1 { get; set; }
        public virtual CountryDistrict CountryDistrict { get; set; }
        public virtual CountryDistrict CountryDistrict1 { get; set; }
        public virtual HREmployee HREmployee { get; set; }
    }
}