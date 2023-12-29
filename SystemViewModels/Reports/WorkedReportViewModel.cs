using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class WorkedReportViewModel : BreadCrumbModel
    {
    }
    public class WorkedReportViewModelList : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public IPagedList<proc_GetTotalWorkedReport_Result> DBModelList { get; set; }
        public ICollection<proc_GetTotalWorkedReport_Result> DBList { get; set; }
    }
}
