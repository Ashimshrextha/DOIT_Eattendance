using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("SystemPermissionByHREmployee")]
    public class SystemPermissionByHREmployeeModel : AuditableEntity<long>
    {
        [Display(Name = "कर्मचारी")]
        public long IdHREmployee { get; set; }
        [Display(Name = "मेनु")]
        public int IdSystemStructure { get; set; }
        public bool Assigned { get; set; }
    }
}
