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
    
    public partial class DeviceLog
    {
        public long Id { get; set; }
        public long DeviceNumber { get; set; }
        public long IdEnroll { get; set; }
        public System.DateTime PunchDate { get; set; }
        public string VerificationMode { get; set; }
        public string InOutMode { get; set; }
        public bool IsProcessed { get; set; }
        public System.DateTime FetchedDate { get; set; }
        public string TerminalIP { get; set; }
    }
}
