using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.CompanyManagement
{
    public class HRCompanyHREmployeeShiftRoasterViewModel : BreadCrumbModel
    {
        public HRCompanyHREmployeeShiftDateModel DataModel { get; set; }
        public List<EmployeeShiftRoasterSelectedModel> DBEmployeeList { get; set; }
        public IEnumerable<proc_GetEmployeeShiftRoasterDetail_Result> DBModelList { get; set; }
        public List<HRCompanyHREmployeeShiftRoasterModel> DataModelList { get; set; }
        public List<proc_GetAssignHREmployeeOfShiftRoaster_Result> DBModelEmp { get; set; }
        public ICollection<HRCompanyHREmployeeShiftRoaster> DBEmpModelList { get; set; }
        public HREmployee DBEmployee { get; set; }
    }
    public class HRCompanyHREmployeeShiftRoasterViewModelList : BreadCrumbModel
    {
        public IPagedList<proc_GetAssignHREmployeeOfShiftRoaster_Result> DBModelEmp { get; set; }
        public IPagedList<HRCompanyHREmployeeShiftDate> DBModelList { get; set; }
        public ICollection<HRCompanyHREmployeeShiftRoaster>DBEmpModelList { get; set; }
    }
}
