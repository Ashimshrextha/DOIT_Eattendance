using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HRLanguage")]
    public class HRLanguageModel : AuditableEntity<long>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "भाषा")]
        [MaxLength(250)]
        public string HRLanguageTitle { get; set; }
    }
}
