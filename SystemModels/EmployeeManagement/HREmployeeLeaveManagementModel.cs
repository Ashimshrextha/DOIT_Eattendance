using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeLeaveManagement")]
    public class HREmployeeLeaveManagementModel : AuditableEntity<int>
    {

        [Display(Name = "कर्मचारी")]
        public long IdHREmployee { get; set; }

        [Display(Name = "बिदा")]
        public int IdMasterLeaveTitle { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "बिदा")]
        [Range(1,double.PositiveInfinity,ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public long IdLeaveType { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "वर्ष")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public int LeaveYearNP { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "कुल बिदा")]
        public decimal TotalLeave { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [MaxLength(500)]
        [Display(Name = "टिप्पणी")]
        public string Remarks { get; set; }
    }
}
