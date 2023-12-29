using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRCompanyLeaveStatusType")]
    public class HRCompanyLeaveStatusTypeModel : AuditableEntity<int>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "अवस्था")]
        [MaxLength(25)]
        public string LeaveStatusType { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "अवस्था (देवनागरी)")]
        [MaxLength(25)]
        public string LeaveStatusTypeNP { get; set; }
    }
}
