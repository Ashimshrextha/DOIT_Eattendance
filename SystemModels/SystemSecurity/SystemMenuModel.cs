using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSecurity
{
    [Table("SystemMenu")]
    public class SystemMenuModel : AuditableEntity<int>
    {
        [Required]
        [Display(Name = "Menu")]
        [MaxLength(250)]
        public string Menu { get; set; }

        [Required]
        [Display(Name = "Area")]
        [MaxLength(250)]
        public string Area { get; set; }

        [Required]
        [Display(Name = "CSS Class")]
        [MaxLength(500)]
        public string CSSClass { get; set; }

        [Required]
        [Display(Name = "Css Icon")]
        [MaxLength(500)]
        public string CSSIcon { get; set; }

        [Required]
        [Display(Name = "Default URL")]
        [MaxLength(250)]
        public string DefaultURL { get; set; }

        [Required]
        [Display(Name = "Order By")]
        public int OrderBy { get; set; }
    }
}
