using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HREmployeeNationalIdentityType")]
    public class HREmployeeNationalIdentityTypeModel : AuditableEntity<int>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "पहिचान प्रकार")]
        [MaxLength(250)]
        public string NationalIdentityType { get; set; }
    }
}
