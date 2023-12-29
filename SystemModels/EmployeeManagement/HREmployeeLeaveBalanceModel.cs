using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeLeaveBalance")]
    public class HREmployeeLeaveBalanceModel : AuditableEntity<int>
    {
        [Display(Name = "कर्मचारी")]
        public long IdHREmployee { get; set; }

        public long IdHRCompanyLeaveType { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "वर्ष")]
        public int LeaveYear { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "जम्मा बिदा")]
        public decimal TotalLeave { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "बिदाको प्रकार")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "Select {0}")]
        public int IdMasterLeaveTitle { get; set; }
    }
}
