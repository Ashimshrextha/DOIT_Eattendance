using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemModels.Auditable;
namespace SystemModels.CompanyManagement
{
    [Table("HREmployeeManualAttendance")]
    public class HREmployeeManualAttendanceModel : AuditableEntity<long>
    {
        public long Id { get; set; }
        public int IdHRCompany { get; set; }
        [Display(Name = "अनुरोध गर्ने ब्यक्ति")]
        public int IdHREmployee { get; set; }

        [Display(Name = "स्विकृत गर्ने ब्यक्ति")]
        public int IdApprovedBy { get; set; }
        [Display(Name = "फाइलको नाम")]
        public string FileName { get; set; }
        [Display(Name = "एक्सल फाइल अपलोड गर्नुहोस")]
        public string FolderName { get; set; }
        [Display(Name = "टिप्पणी")]
        public string Remarks { get; set; }
        [Display(Name = "स्थिति")]
        public string Status { get; set; }
    }
}

