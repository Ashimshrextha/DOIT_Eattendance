using System.ComponentModel.DataAnnotations;

namespace SystemModels.CompanyManagement
{
    public class EmployeeShiftRoasterSelectedModel
    {
        public long IdHREmployee { get; set; }

        public string PISNumber { get; set; }

        public string EmployeeName { get; set; }

        public string DesignationTitle { get; set; }

        public string DivisionName { get; set; }

        [Required]
        public bool IsChecked { get; set; } = false;
    }
}
