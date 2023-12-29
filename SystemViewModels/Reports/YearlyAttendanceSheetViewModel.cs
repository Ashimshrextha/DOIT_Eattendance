using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class YearlyAttendanceSheetViewModel: BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }

        public ICollection<proc_GetYearlyAttendanceSheetReport_Result> DBModelList { get; set; }
    }
}
