using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeEducationType")]
    public class HREmployeeEducationTypeModel : AuditableEntity<int>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "डिग्री नाम")]
        [MaxLength(100)]
        public string EducationType { get; set; }
    }
}
