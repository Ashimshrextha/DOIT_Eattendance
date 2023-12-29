using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HREmployeeRelationType")]
    public class HREmployeeRelationTypeModel : AuditableEntity<int>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "सम्बन्ध प्रकार")]
        [MaxLength(100)]
        public string RelationType { get; set; }
    }
}
