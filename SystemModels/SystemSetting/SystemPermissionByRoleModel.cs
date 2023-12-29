using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("SystemPermissionByRole")]
    public class SystemPermissionByRoleModel : AuditableEntity<long>
    {
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "भूमिका नाम")]
        public int IdRole { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "नैभ बार मेनु")]
        public int IdSystemStructure { get; set; }

        [Display(Name = "असाइनइड")]
        public bool Assigned { get; set; }
    }
}
