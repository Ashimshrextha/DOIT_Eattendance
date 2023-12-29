using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeAddress")]
    public class HREmployeeAddressModel : AuditableEntity<long>
    {
        [Display(Name = "कर्मचारी")]
        public long IdHREmployee { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "स्थायी शहर")]
        public long IdPermanentCity { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "स्थायी जिल्ला")]
        public long IdPermanentDistrict { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "अस्थायी जिल्ला")]
        public long IdTemporaryDistrict { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "अस्थायी शहर")]
        public long IdTemporaryCity { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "स्थायी वार्ड नं.")]
        public string PermanentWardNumber { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "अस्थायी वार्ड नं.")]
        public string TemporaryWardNumber { get; set; }

        [Display(Name = "टिप्पणी")]
        [MaxLength(500)]
        public string Remark { get; set; }

        [Display(Name = "पूर्वनिर्धारित छ/छैन")]
        public bool IsDefault { get; set; }
    }
}
