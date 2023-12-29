using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.DeviceManagement
{
    [Table("BiometricTemplateTypeModel")]
    public class BiometricTemplateTypeModel : AuditableEntity<int>
    {
        [Required]
        [Display(Name = "टेम्प्लेट शीर्षक")]
        public string BiometricTemplateTypeTitle { get; set; }
    }
}
