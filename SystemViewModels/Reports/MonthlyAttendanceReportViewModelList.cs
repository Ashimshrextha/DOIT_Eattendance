using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class MonthlyAttendanceReportViewModelList : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public ICollection<proc_GetMonthlyAttendanceHeader_Result> DBReportHeader { get; set; }
        public ICollection<proc_MonthlyAttendance_Result> DBModelPrint { get; set; }
        public IPagedList<proc_MonthlyAttendance_Result> DBModel { get; set; }
    }
}
