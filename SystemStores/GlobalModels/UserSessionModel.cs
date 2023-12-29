using System;

namespace SystemStores.GlobalModels
{
    public class UserSessionModel
    {
        public string IdSessoin { get; set; }
        public Guid IdLogin { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool Locked { get; set; }
        public DateTime? LockOutEndDate { get; set; }
        public long? IdEnroll { get; set; }
        public string Language { get; set; }
        public long? IdGender { get; set; }
        public long? IdMarital { get; set; }
        public string Address { get; set; }
        public DateTime? DOB { get; set; }
        public long? IdHREmployee { get; set; }
        public long? IdHRCompany { get; set; }
        public string HRCompanyName { get; set; }
        public string ParentHRCompanyName { get; set; }
        public int? IdRoleType { get; set; }
        public int? IdHREmployeeRole { get; set; }
        public long? IdHREmployeeServiceType { get; set; }
        public long? IdHRCompanyDivision { get; set; }
        public int? IdHREmployeeJobStatus { get; set; }
        public bool? IsHead { get; set; }
        public string HRCompanyLogo { get; set; }
    }
}
