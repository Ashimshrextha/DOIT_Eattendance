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
    
    public partial class HRDeviceModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRDeviceModel()
        {
            this.HRDevice = new HashSet<HRDevice>();
            this.HRDevice1 = new HashSet<HRDevice>();
            this.HRDeviceModelUserVerficationMode = new HashSet<HRDeviceModelUserVerficationMode>();
            this.HRDeviceModelUserVerficationMode1 = new HashSet<HRDeviceModelUserVerficationMode>();
            this.HREmployeeBiometricTemplate = new HashSet<HREmployeeBiometricTemplate>();
        }
    
        public int Id { get; set; }
        public string HRDeviceModel1 { get; set; }
        public bool IsRealTime { get; set; }
        public bool IsActive { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRDevice> HRDevice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRDevice> HRDevice1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRDeviceModelUserVerficationMode> HRDeviceModelUserVerficationMode { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRDeviceModelUserVerficationMode> HRDeviceModelUserVerficationMode1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HREmployeeBiometricTemplate> HREmployeeBiometricTemplate { get; set; }
    }
}
