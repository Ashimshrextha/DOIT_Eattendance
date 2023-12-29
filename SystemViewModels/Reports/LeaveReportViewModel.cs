using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class LeaveReportViewModel : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public ICollection<proc_GetMonthlyAttendanceHeader_Result> DBReportHeader { get; set; }
        public ICollection<proc_GetMonthlyLeaveReport_Result> DBModelList { get; set; }
    }
    public class LeaveReportViewModelList : BreadCrumbModel
    {
        public ICollection<proc_GetMonthlyAttendanceHeader_Result> DBReportHeader { get; set; }
        public IPagedList<proc_GetMonthlyLeaveReport_Result> DBModelList { get; set; }
    }
}
