using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.CompanyManagement
{
    public class HRCompanyHREmployeeBulkKaajViewModel : BreadCrumbModel
    {
        public HRCompanyHREmployeeShiftDateModel DataModel1 { get; set; }
        public HRCompanyHREmployeeBulkKaajModel DataModel { get; set; }
        public List<EmployeeShiftRoasterSelectedModel> DBEmployeeList { get; set; }
        public IEnumerable<proc_GetEmployeeShiftRoasterDetail_Result> DBModelList { get; set; }
        public List<HRCompanyHREmployeeBulkKaajModel> DataModelList { get; set; }
        public List<proc_GetAssignHREmployeeOfShiftRoaster_Result> DBModelEmp { get; set; }
        public ICollection<HREmployeeKaajHistory> DBEmpModelList { get; set; }
        public HREmployee DBEmployee { get; set; }
        public IEnumerable<string> ddlKajVehicalSelected { get; set; }
    }
    public class HRCompanyHREmployeeBulkKaajViewModelList : BreadCrumbModel
    {
        //public IPagedList<proc_GetAssignHREmployeeOfShiftRoaster_Result> DBModelEmp { get; set; }
         public IPagedList<HREmployeeKaajHistory> DBModelEmp { get; set; }
        public ICollection<HREmployeeKaajHistory>DBEmpModelList { get; set; }
    }
}
