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
    
    public partial class HRCompanyDeviceMonitor
    {
        public long Id { get; set; }
        public long IdDevice { get; set; }
        public string EventMessage { get; set; }
        public bool IsOnline { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        public virtual HRDevice HRDevice { get; set; }
    }
}
