using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class LateAttendanceReportViewModelList : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public HREmployeeAttendanceHistoryModel DataModel { get; set; }
        public ICollection<proc_LateAttendance_Result> DBModelList { get; set; }
        public IPagedList<proc_LateAttendance_Result> DBModelPageList { get; set; }
        public proc_GetEmployeeDetail_Result DBModelEmployee { get; set; }
    }
}
