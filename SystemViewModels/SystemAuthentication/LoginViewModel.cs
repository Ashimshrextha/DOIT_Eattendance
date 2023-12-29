using System;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemAuthentication;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemAuthentication
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
    public class LoginViewModel : BreadCrumbModel
    {
        public LoginModel DataModel { get; set; }
        public IEnumerable<proc_GetEmployeeTodayStatusForDashboard_Result> DbEmployeeTodayStatus { get; set; }
        public IEnumerable<proc_GetEmployeeCountByGender_Result> DbGender { get; set; }
        public RegisterViewModel DataModelRegister { get; set; }
        public Nullable<int> OfficeCount { get; set; }
        public Nullable<int> ActiveOfficeCount { get; set; }
        public Nullable<int> EmployeeCount { get; set; }
        public Nullable<int> EmployeePresentCount { get; set; }
        public Nullable<int> DeviceCount { get; set; }
        public Nullable<int> ActiveDeviceCount { get; set; }
    }
}
