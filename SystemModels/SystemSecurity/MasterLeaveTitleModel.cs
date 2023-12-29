using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSecurity
{
    [Table("MasterLeaveTitle")]
    public class MasterLeaveTitleModel : AuditableEntity<int>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "बिदा प्रकार (अंग्रेजी)")]
        [MaxLength(250)]
        public string LeaveTitle { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "बिदा प्रकार (देवनागरी)")]
        [MaxLength(250)]
        public string LeaveTitleNP { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "बिदा प्रकार छोटो शीर्षक")]
        [MaxLength(15)]
        public string LeaveShortTitle { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "रङ कोड")]
        [MaxLength(250)]
        public string ColorCode { get; set; }
    }
}
