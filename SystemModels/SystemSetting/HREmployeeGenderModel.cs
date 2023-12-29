using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HREmployeeSex")]
    public class HREmployeeGenderModel:AuditableEntity<long>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "लिङ्ग")]
        [MaxLength(25)]
        public string HREmployeeSexTitle { get; set; }
    }
}
