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
    
    public partial class HREmployeeBiometricTemplate
    {
        public long Id { get; set; }
        public long IdHREmployee { get; set; }
        public int IdTemplateType { get; set; }
        public long IdEnroll { get; set; }
        public Nullable<int> TemplateIndex { get; set; }
        public Nullable<long> TemplateDataLength { get; set; }
        public string TemplateDataStr { get; set; }
        public byte[] TemplateDataBin { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual BiometricTemplateType BiometricTemplateType { get; set; }
        public virtual HREmployee HREmployee { get; set; }
    }
}