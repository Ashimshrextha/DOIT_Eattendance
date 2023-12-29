using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HREmployeeJobStatus")]
    public class HREmployeeJobStatusModel : AuditableEntity<int>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "अवस्था")]
        [MaxLength(100)]
        public string HREmployeeJobStatusTitle { get; set; }
    }
}
