using PagedList;
using System.Collections.Generic;
using System.Web;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemModels.SystemAuthentication;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeViewModel : BreadCrumbModel
    {
        public HREmployee DBModel { get; set; }
        public HREmployeeModel DataModel { get; set; }
        public LoginsModel DataModelLogin { get; set; }
        public MembershipProviderModel DataModelMembership { get; set; }
        public HREmployeeNationalIdentityModel DataModelNIdentity { get; set; }
        public ICollection<proc_GetEmployeeByDesignation_Result> DBModelEmp { get; set; }
        public ICollection<HREmployee> DBModelList { get; set; }
        public HttpPostedFileBase ImageProfile { get; set; }
        public HttpPostedFileBase NationalIdentity { get; set; }
        public ReportHeaderViewModel Header { get; set; }
    }
    public class HREmployeeViewModelList : BreadCrumbModel
    {
        public HREmployee DBModel { get; set; }
        public IPagedList<proc_GetEmployeeByDesignation_Result> DBModelEmp { get; set; }
        public IPagedList<HREmployee> DBModelList { get; set; }
    }
}
