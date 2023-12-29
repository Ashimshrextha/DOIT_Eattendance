using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class KaajReportViewModel : BreadCrumbModel
    {
        public ICollection<proc_GetMonthlyAttendanceHeader_Result> DBReportHeader { get; set; }
        public ICollection<proc_GetMonthlyKaajReport_Result> DBModelKaajList { get; set; }
        public ReportHeaderViewModel Header { get; set; }
        public HREmployeeKaajHistoryModel DataModel { get; set; }
        public ICollection<HREmployeeKaajHistory> DBModelList { get; set; }
    }
    public class KaajReportViewModelList : BreadCrumbModel
    {
        public ICollection<proc_GetMonthlyAttendanceHeader_Result> DBReportHeader { get; set; }
        public ReportHeaderViewModel Header { get; set; }
        public HREmployeeKaajHistory DBModel { get; set; }
        public IPagedList<HREmployeeKaajHistory> DBModelList { get; set; }
        public IPagedList<proc_GetMonthlyKaajReport_Result> DBModelKaajList { get; set; }
    }
}
