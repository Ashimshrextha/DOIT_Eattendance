using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HREmployeeContactType")]
    public class HREmployeeContactTypeModel : AuditableEntity<long>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "सम्पर्क प्रकार")]
        [MaxLength(50)]
        public string ContactTypeTitle { get; set; }

    }
}
