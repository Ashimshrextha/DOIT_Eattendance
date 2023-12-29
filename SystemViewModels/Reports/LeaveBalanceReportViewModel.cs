using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class LeaveBalanceReportViewModel : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public proc_GetEmployeeDetail_Result DBModel { get; set; }
        public HREmployeeModel DataModel { get; set; }
        public ICollection<proc_GetHREmployeeLeaveHistoryPerYearReport_Result> DBModelList { get; set; }
        public ICollection<HREmployeeLeaveManagement> LeaveMgmtList { get; set; }
    }
    public class LeaveBalanceReportViewModelList : BreadCrumbModel
    {
        public IPagedList<proc_GetBalanceLeaveReport_Result> DBModelPageList { get; set; }
        public ICollection<proc_GetHREmployeeLeaveHistoryPerYearReport_Result> DBModelList { get; set; }
    }
}
