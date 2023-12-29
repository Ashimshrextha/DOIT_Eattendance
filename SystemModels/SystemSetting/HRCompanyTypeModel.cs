using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HRCompanyType")]
    public class HRCompanyTypeModel : AuditableEntity<int>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "कार्यालयको प्रकार")]
        [MaxLength(50)]
        public string CompanyType { get; set; }

        [Display(Name = "प्रमुख कार्यालय प्रकार")]
        public Nullable<int> IdParent_CompanyType { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "कार्यालयको प्रकार (छोटो नाम)")]
        [MaxLength(25)]
        public string CompanyTypeShortName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "कार्यालयको श्रेणी मिलाउनुहोस्")]
        public int OrderBy { get; set; }
    }
}
