using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.Reports;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class EarlyCheckoutAttendanceReportViewModel : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public EarlyCheckoutAttendanceReportModel DataModel { get; set; }
        public proc_EarlyCheckoutReport_Result DBModel { get; set; }
        public proc_GetEmployeeDetail_Result DBModelEmployee { get; set; }
    }
    public class EarlyCheckoutAttendanceReportViewModelList : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public IPagedList<proc_EarlyCheckoutReport_Result> DBModelPageList { get; set; }
        public ICollection<proc_EarlyCheckoutReport_Result> DBModelList { get; set; }
    }
}
