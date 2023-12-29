using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HREmployeeBankList")]
    public class HREmployeeBankListModel : AuditableEntity<long>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "बैंक नाम")]
        [MaxLength(500)]
        public string BankName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "बैंकको प्रकार")]
        [MaxLength(2)]
        public string BankCategory { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "बैंक स्थान")]
        [MaxLength(250)]
        public string BankLocation { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "फोन")]
        [MaxLength(100)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "फैक्स")]
        [MaxLength(100)]
        public string Fax { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "स्विफ्ट कोड")]
        [MaxLength(100)]
        public string SwiftCode { get; set; }

        [Display(Name = "आईबीएएन")]
        [MaxLength(100)]
        public string IBAN { get; set; }
    }
}
