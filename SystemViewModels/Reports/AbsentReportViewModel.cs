using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class AbsentReportViewModelList : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public ICollection<proc_EmployeeAbsentReport_Result> DBModelList { get; set; }
        public IPagedList<proc_EmployeeAbsentReport_Result> DBModelPageList { get; set; }
    }
}
