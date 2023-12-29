using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeContact")]
    public class HREmployeeContactModel : AuditableEntity<long>
    {
        [Display(Name = "कर्मचारी")]
        public long? IdHREmployee { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "सम्पर्क प्रकार")]
        public long IdContactType { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "सम्पर्क नं.")]
        [MaxLength(10)]
        public string ContactNumber { get; set; }

        [Display(Name = "पुर्बनिर्धरित छ/छैन")]
        public bool IsDefault { get; set; }

    }
}
