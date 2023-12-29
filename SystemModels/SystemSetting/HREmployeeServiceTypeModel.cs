using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HREmployeeServiceType")]
    public class HREmployeeServiceTypeModel : AuditableEntity<long>
    {
       [Display(Name ="कार्यालय")]
        public long IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "सेवाको शीर्षक")]
        [ MaxLength(250)]
        public string HREmployeeServiceTypeTitle { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "सेवा शीर्षकको छोटो नाम")]
        [MaxLength(100)]
        public string HREmployeeServiceTypeShortName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "अनुक्रम")]
        public int HREmployeeServiceTypeOrder { get; set; }
      
        [Display(Name = "स्थायी सेवा छ/छैन")]
        public bool IsPermanentServiceType { get; set; }
    }
}
