using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRCompanyHREmployeeShiftRoaster")]
    public class HRCompanyHREmployeeShiftRoasterModel : AuditableEntity<long>
    {
        public long IdHRCompanyHREmployeeShiftDate { get; set; }

        public int IdDayOfWeekNP { get; set; }

        public Nullable<long> IdHRCompanyShiftRoaster { get; set; }

        public bool IsWeekend { get; set; }

        public string DOWNP { get; set; }

        public List<long> DDLShiftRoasterSelected { get; set; }
    }
}
