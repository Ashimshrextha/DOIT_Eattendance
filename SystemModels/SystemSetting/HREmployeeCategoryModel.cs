using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HREmployeeCategory")]
    public class HREmployeeCategoryModel : AuditableEntity<long>
    {
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "श्रेणी शीर्षक")]
        [MaxLength(50)]
        public string HREmployeeCategoryTitle { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "श्रेणी शीर्षक (छोटो नाम)")]
        [MaxLength(25)]
        public string HREmployeeCategoryShortName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "उच्च स्तरको पोस्ट छ/छैन")]
        public bool IsHigherLevelOfficer { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "मध्य स्तरको पोस्ट छ/छैन")]
        public bool IsMidLevelOfficer { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "तल्लो स्तरको पोस्ट छ/छैन")]
        public bool IsLowLevelOfficer { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "प्रकारको अनुक्रम")]
        public int HREmployeeCategoryOrder { get; set; }
    }
}
