using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRCompanyDivision")]
    public class HRCompanyDivisionModel : AuditableEntity<long>
    {
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "शाखा/सेक्सन प्रकार")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public long IdDivisionType { get; set; }


        [Display(Name = "प्रमुख शाखा/सेक्सन")]
        public Nullable<long> IdParent_HRCompanyDivision { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "शाखा/सेक्सन नाम")]
        [MaxLength(250)]
        public string HRCompanyDivisionName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "शाखा/सेक्सन नाम(संक्षिप्त)")]
        [MaxLength(10)]
        public string HRCompanyDivisionShortName { get; set; }
    }
}
