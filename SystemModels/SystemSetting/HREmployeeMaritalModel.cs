using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HREmployeeMarital")]
    public class HREmployeeMaritalModel:AuditableEntity<long>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "वैवाहिक स्थिति")]
        [MaxLength(100)]
        public string HREmployeeMaritalTitle { get; set; }
       
    }
}
