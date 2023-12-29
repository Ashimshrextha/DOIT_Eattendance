using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HREmployeeRole")]
    public class HREmployeeRoleModel : AuditableEntity<int>
    {
        [Display(Name = "कार्यालय")]
        public long? IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "भूमिका प्रकार")]
        public int IdRoleType { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "भूमिका")]
        [MaxLength(15)]
        public string HRRoleTitle { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "सिस्टम परिभाषित छ/छैन")]
        public bool IsSystemDefined { get; set; }
    }
}
