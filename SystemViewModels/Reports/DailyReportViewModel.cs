using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class DailyReportViewModelList : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public ICollection<proc_DailyAttendanceReport_Result> DBModelList { get; set; }
        public IPagedList<proc_DailyAttendanceReport_Result> DBModelPageList { get; set; }
    }
}
