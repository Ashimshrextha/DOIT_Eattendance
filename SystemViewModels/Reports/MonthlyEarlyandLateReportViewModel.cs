using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{

    public class MonthlyEarlyandLateReportViewModel : BreadCrumbModel
    {

        public ReportHeaderViewModel Header { get; set; }

        public ICollection<proc_GetMonthlyWorkingSummaryReport_Result> DBModelList { get; set; }

        public ICollection<proc_GetMonthlyWorkingDetailReport_Result> DBModelAttendanceList  { get; set; }

        public ICollection<proc_GetDateRangeWorkingSummaryReport_Result> DBModelAttendanceRangeList { get; set; }
        public ICollection<proc_GetDateRangeWorkingDetailReport_Result> DBModelAttendanceWorkingRangeList { get; set; }
    }
    public class MonthlyEarlyandLateReportViewModellist : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }

        public IPagedList<proc_GetMonthlyWorkingSummaryReport_Result> DBModelList { get; set; }
        public IPagedList<proc_GetDateRangeWorkingSummaryReport_Result> DBModelListRange { get; set; }
    }
}
    

