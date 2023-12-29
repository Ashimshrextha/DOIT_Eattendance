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
    
    public partial class HRCompanyNotification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRCompanyNotification()
        {
            this.HRCompanyAssignNotifications = new HashSet<HRCompanyAssignNotification>();
        }
    
        public long Id { get; set; }
        public long IdHRCompany { get; set; }
        public long IdNoticeBy { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationTitleNP { get; set; }
        public string NotificationMessage { get; set; }
        public bool IsActive { get; set; }
        public bool IsGeneral { get; set; }
        public System.DateTime EffectiveFromDate { get; set; }
        public System.DateTime EffectiveToDate { get; set; }
        public string TerminalIP { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRCompanyAssignNotification> HRCompanyAssignNotifications { get; set; }
        public virtual HRCompany HRCompany { get; set; }
        public virtual HREmployee HREmployee { get; set; }
    }
}
