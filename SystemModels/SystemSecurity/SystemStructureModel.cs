using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSecurity
{
    [Table("SystemStructure")]
    public class SystemStructureModel : AuditableEntity<int>
    {
        [Display(Name = "Nav Bar Menu")]
        public int IdSystemMenu { get; set; }

        [Display(Name = "Parent Menu")]
        public int IdParent_SystemStructure { get; set; }

        [Required]
        [Display(Name = "Menu Title")]
        [MaxLength(250)]
        public string Header { get; set; }

        [Display(Name = "Controller")]
        [MaxLength(250)]
        public string Controller { get; set; }

        [Display(Name = "Action Name")]
        [MaxLength(250)]
        public string ActionName { get; set; }

        [Required]
        [Display(Name = "CSS Class")]
        [MaxLength(500)]
        public string CSSClass { get; set; }

        [Display(Name = "CSS Class 1")]
        [MaxLength(500)]
        public string CSSClass1 { get; set; }

        [Required]
        [Display(Name = "CSS Icon")]
        [MaxLength(500)]
        public string CSSIcon { get; set; }

        [Display(Name = "CSS Icon 1")]
        [MaxLength(500)]
        public string CSSIcon1 { get; set; }

        [Required]
        [Display(Name = "Parent Order By")]
        public int ParentOrder { get; set; }

        [Display(Name = "Child Order By")]
        public Nullable<int> ChildOrder { get; set; }

        [Required]
        [Display(Name = "Is Tab")]
        public bool IsTabbed { get; set; }

        [Required]
        [Display(Name = "Is Menu")]
        public bool IsMenu { get; set; }
    }
}
