//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SystemDatabase
{
    using System;
    
    public partial class proc_EmployeeAbsentReport_Result
    {
        public long IdHRCompany { get; set; }
        public long IdHREmployee { get; set; }
        public string HREmployeeName { get; set; }
        public string HREmployeeNameNP { get; set; }
        public string AppointmentDateNP { get; set; }
        public string MobileNumber { get; set; }
        public string PISNumber { get; set; }
        public Nullable<long> IdEnroll { get; set; }
        public string HRCompanyDivisionShortName { get; set; }
        public string HRCompanyDivisionName { get; set; }
        public string HRDesignationShortName { get; set; }
        public int HRDesignationRank { get; set; }
        public string HRDesignationTitle { get; set; }
        public Nullable<System.DateTime> AbsentDate { get; set; }
        public string EmployeeStatus { get; set; }
    }
}
