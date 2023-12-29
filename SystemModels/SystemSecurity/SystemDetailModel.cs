using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSecurity
{
    [Table("SystemDetail")]
    public class SystemDetailModel : AuditableEntity<int>
    {
        [Display(Name = "प्रकार")]
        public int IdSystemDetailCategory { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "सारांश")]
        public string Summary { get; set; }

        [Display(Name = "अपलोड डकुमेन्ट")]
        [MaxLength(250)]
        public string Document { get; set; }

        [Display(Name = "अपलोड")]
        [MaxLength(250)]
        public string Document1 { get; set; }

    }
}
