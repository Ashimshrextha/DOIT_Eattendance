using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;
using SystemDatabase;
using PagedList;

namespace SystemViewModels.Reports
{

    public class DatePunchedReportViewModel : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public IPagedList<proc_GetEmployeeByDesignation_Result> DBModelEmp { get; set; }
        public IPagedList<proc_GetMonthlyWorkingSummaryReport_Result> DBModelList { get; set; }
        public ICollection<proc_GetMonthlyWorkingSummaryReport_Result> DBModelCollection { get; set; }
    }


}
