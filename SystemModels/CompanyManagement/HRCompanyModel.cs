using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRCompany")]
    public class HRCompanyModel : AuditableEntity<long>
    {
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "कार्यालय कोड")]
        public int IdCompanyCode { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "कार्यालयको प्रकार")]
        public int IdHRCompanyType { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "प्रमुख कार्यालय")]
        public Nullable<long> IdParent_HRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "प्रदेश")]
        public int IdState { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "कार्यालयको नाम (अंग्रेजीमा)")]
        [MaxLength(500)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "कार्यालयको नाम (देवनागरी)")]
        [MaxLength(500)]
        public string CompanyNameNP { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "कार्यालयको नाम(संक्षिप्त)")]
        [MaxLength(10)]
        public string CompanyShortName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "ठेगाना")]
        [MaxLength(500)]
        public string Address { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "फोन नं.")]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        [Display(Name = "फैक्स नं.")]
        [MaxLength(100)]
        public string FaxNumber { get; set; }

        [Display(Name = "टिप्पणी")]
        [MaxLength(500)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "केन्द्रीय कार्यालय")]
        public bool IsCentralLevelHRCompany { get; set; }

        [Display(Name = "कार्यालय लोगो")]
        [MaxLength(1000)]
        public string Logo { get; set; }
    }
}
