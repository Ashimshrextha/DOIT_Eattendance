using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class ShiftRoasterReportViewModelList: BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public ICollection<proc_GetEmployeeShiftRoasterReport_Result> DBModelList { get; set; }
    }
}
