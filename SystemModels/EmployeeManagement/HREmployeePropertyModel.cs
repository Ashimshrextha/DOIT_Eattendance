using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeProperty")]
    public class HREmployeePropertyModel : AuditableEntity<long>
    {
        [Display(Name = "कर्मचारी")]
        public long IdHREmployee { get; set; }

        [Required]
        [Display(Name = "सम्पत्तीको प्रकार")]
        [MaxLength(250)]
        public string HREmployeeProperty1 { get; set; }

        [Required]
        [Display(Name = "विस्तार")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Display(Name = "अपलोड")]
        [MaxLength(250)]
        public string Document { get; set; }

        [Display(Name = "अपलोड")]
        [MaxLength(250)]
        public string Document1 { get; set; }

        [Display(Name = "अपलोड")]
        [MaxLength(250)]
        public string Document2 { get; set; }
    }
}
