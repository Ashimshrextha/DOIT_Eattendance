using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HREmployeeBloodGroup")]
    public class HREmployeeBloodGroupModel : AuditableEntity<int>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "रगतको समूह")]
        [MaxLength(5)]
        public string HREmployeeBloodGroupTitle { get; set; }
    }
}
