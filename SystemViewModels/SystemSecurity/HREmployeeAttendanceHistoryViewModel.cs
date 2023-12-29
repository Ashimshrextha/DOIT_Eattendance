using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSecurity
{
    public class HREmployeeAttendanceHistoryViewModel : BreadCrumbModel
    {
        public HREmployeeAttendanceHistoryModel DataModel { get; set; }
        public IPagedList<HREmployeeAttendanceHistoryModel> DataModelList { get; set; }
        public ICollection<HREmployeeAttendanceHistory> DBModelList { get; set; }
        public proc_GetEmployeeDetail_Result DBModelEmployee { get; set; }
    }
    public class HREmployeeAttendanceHistoryViewModelList : BreadCrumbModel
    {
        public HREmployeeAttendanceHistory DBModel { get; set; }

        public IPagedList<HREmployeeAttendanceHistory> DBModelList { get; set; }
    }
}
