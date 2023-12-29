using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HREmployeeServiceTypeGroup")]
    public class HREmployeeServiceTypeGroupModel : AuditableEntity<long>
    {
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "सेवा प्रकार")]
        public long IdServiceType { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "समूहको नाम")]
        [MaxLength(250)]
        public string GroupName { get; set; }
    }
}
