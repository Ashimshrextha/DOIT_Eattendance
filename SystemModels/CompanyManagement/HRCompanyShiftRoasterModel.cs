using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRCompanyShiftRoaster")]
    public class HRCompanyShiftRoasterModel : AuditableEntity<long>
    {

        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "शिफ्ट शीर्षक")]
        [MaxLength(250)]
        public string ShiftTitle { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "लगइन समय")]
        [DataType(DataType.Time)]
        public System.TimeSpan LoginTime { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "लगआउट समय")]
        [DataType(DataType.Time)]
        public System.TimeSpan LogoutTime { get; set; }
    }
}
