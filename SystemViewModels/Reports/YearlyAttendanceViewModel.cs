using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class YearlyAttendanceViewModel : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public ICollection<proc_GetYearMonthlyAttendanceReport_Result> DBModelList { get; set; }
    }
    public class YearlyAttendanceViewModelList : BreadCrumbModel
    {
        public ICollection<proc_GetYearMonthlyAttendanceReport_Result> DBModel { get; set; }
    }
}
