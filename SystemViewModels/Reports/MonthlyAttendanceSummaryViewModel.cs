using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class MonthlyAttendanceSummaryViewModel : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }

        public ICollection<proc_GetMonthlyWorkingSummaryReport_Result> DBModelList { get; set; }

        public ICollection<proc_GetMonthlyWorkingDetailReport_Result> DBModelAttendanceList { get; set; }
    }
    public class MonthlyAttendanceSummaryViewModelList : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }

        public IPagedList<proc_GetMonthlyWorkingSummaryReport_Result> DBModelList { get; set; }
    }
}
