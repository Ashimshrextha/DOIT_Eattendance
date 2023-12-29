using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSecurity
{
    [Table("SystemDetailCategory")]
    public class SystemDetailCategoryModel : AuditableEntity<int>
    {
        [Required]
        [Display(Name = "प्रकार")]
        [MaxLength(250)]
        public string CategoryTitle { get; set; }
    }
}
