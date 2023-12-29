using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.DeviceManagement
{
    [Table("HRDeviceModelUserVerficationMode")]
    public class HRDeviceModelUserVerficationModeModel : AuditableEntity<int>
    {
        [Required]
        [Display(Name = "उपकरण मोडेल")]
        public int IdHRDeviceModel { get; set; }

        [Required]
        [Display(Name = "मोड")]
        [MaxLength(250)]
        public string ModeTitle { get; set; }

        [Required]
        [Display(Name = "मोड रेन्ज")]
        public int ModeRange { get; set; }
    }
}
