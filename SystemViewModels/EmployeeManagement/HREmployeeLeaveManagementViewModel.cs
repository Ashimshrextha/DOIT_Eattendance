using PagedList;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeLeaveManagementViewModel:BreadCrumbModel
    {
        public HREmployeeLeaveManagementModel DataModel { get; set; }
        public IPagedList<HREmployeeLeaveManagement> DBModelList { get; set; }
    }
    public class HREmployeeLeaveManagementViewModelList : BreadCrumbModel
    {
        public IPagedList<HREmployeeLeaveManagement> DBModelList { get; set; }
    }
}
