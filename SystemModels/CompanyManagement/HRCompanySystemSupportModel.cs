using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRCompanySystemSupport")]
    public class HRCompanySystemSupportModel : AuditableEntity<long>
    {
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "सम्पर्क व्यक्ति")]
        [MaxLength(250)]
        public string ContactPerson { get; set; }

        [Display(Name = "मोबाइल नं.")]
        [RegularExpression("^([7-9]{3})?[0-9]*$", ErrorMessage = "कृपया सही  {0} लेख्नुहोस्")]
        [MinLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस्")]
        [MaxLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस्")]
        public string MobileNumber { get; set; }

        [Display(Name = "फोन नं.")]
        [MaxLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस्")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [DataType(DataType.EmailAddress, ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "इमेल")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "टिप्पणी")]
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
