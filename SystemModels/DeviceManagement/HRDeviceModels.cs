using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.DeviceManagement
{
    [Table("HRDeviceModel")]
    public class HRDeviceModels : AuditableEntity<int>
    {
        [Required]
        [Display(Name = "उपकरण मोडेल")]
        [MaxLength(250)]
        public string HRDeviceModel1 { get; set; }

        [Required]
        [Display(Name = "स्वत: पुश प्रविधि")]
        public bool IsRealTime { get; set; }
    }
}
