using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class MonthlyAttendanceSheetViewModelList : BreadCrumbModel
    {
        public ICollection<proc_GetMonthlyAttendanceHeader_Result> DBReportHeader { get; set; }
        public ReportHeaderViewModel Header { get; set; }
        public ICollection<proc_GetMonthlyAttendanceTimeSheetReport_Result> DBModelPrint { get; set; }
        public IPagedList<proc_GetMonthlyAttendanceTimeSheetReport_Result> DBModel { get; set; }
    }
}
