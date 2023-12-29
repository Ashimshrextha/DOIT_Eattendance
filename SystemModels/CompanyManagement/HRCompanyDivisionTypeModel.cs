using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRCompanyDivisionType")]
    public class HRCompanyDivisionTypeModel : AuditableEntity<long>
    {
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "शाखा/सेक्सन प्रकार")]
        [MaxLength(25)]
        public string HRCompanyDivisionTypeTitle { get; set; }
    }
}
