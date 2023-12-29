using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HRLanguageConversion")]
    public class HRLanguageConversionModel : AuditableEntity<int>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "अंग्रेजी शीर्षक")]
        [MaxLength(500)]
        public string English { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "नेपाली शीर्षक")]
        [MaxLength(500)]
        public string Nepali { get; set; }

        [Display(Name = "मैथली शीर्षक")]
        [MaxLength(500)]
        public string Maithali { get; set; }

        [Display(Name = "नेवारी शीर्षक")]
        [MaxLength(500)]
        public string Newari { get; set; }

        [Display(Name = "अन्य शीर्षक")]
        [MaxLength(500)]
        public string Other { get; set; }
    }
}
