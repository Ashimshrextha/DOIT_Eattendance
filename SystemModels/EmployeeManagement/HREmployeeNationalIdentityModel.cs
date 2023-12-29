using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeNationalIdentity")]
    public class HREmployeeNationalIdentityModel : AuditableEntity<long>
    {
        [Display(Name = "कर्मचारी")]
        public long IdHREmployee { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "पहिचान प्रकार")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public int IdNationalIdentityType { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "पहिचान नं.")]
        [MaxLength(20, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस")]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "जारी जिल्ला")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public long IssueDistrict { get; set; }

        [Display(Name = "जारी मिति")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> IssueDate { get; set; }

        [Display(Name = "समाप्ति मिति")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> ExpiryDate { get; set; }

        [Display(Name = "अपलोड पहिचान")]
        [MaxLength(250)]
        public string Document { get; set; }

        [Display(Name = "कागजात अपलोड")]
        [MaxLength(250)]
        public string Document1 { get; set; }

        [Display(Name = "अपलोड")]
        [MaxLength(250)]
        public string Document2 { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "पुर्बनिर्धरित छ/छैन")]
        public bool IsDefault { get; set; } = true;

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "जारी मिति")]
        [MinLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस")]
        [MaxLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस")]
        public string IssueDateNP { get; set; }

        [Display(Name = "समाप्ति मिति")]
        [MinLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस")]
        [MaxLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस")]
        public string ExpiryDateNP { get; set; }
    }
}
