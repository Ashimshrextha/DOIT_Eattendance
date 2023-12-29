using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRCompanyDeviceMonitor")]
    public class HRCompanyDeviceMonitorModel : EntityId<long>
    {
        [Required]
        [Display(Name = "उपकरण")]
        public long IdDevice { get; set; }

        [Required]
        [Display(Name = "सन्देश")]
        [MaxLength(500)]
        public string EventMessage { get; set; }

        [Required]
        [Display(Name = "अनलाइन छ/छैन")]
        public bool IsOnline { get; set; }

        [Required]
        [Display(Name = "सिर्जना मिति")]
        [DataType(DataType.DateTime)]
        public System.DateTime CreatedOn { get; set; }
    }
}
