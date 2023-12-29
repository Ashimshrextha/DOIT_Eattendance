using PagedList;
using System.Collections.Generic;
using System.Web;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeLeaveHistoryViewModel : BreadCrumbModel
    {
        public virtual HREmployeeLeaveCancelModel DataModelLeaveCancel { get; set; }

        public virtual HREmployeeLeaveHistoryModel DataModel { get; set; }

        public ICollection<HREmployeeLeaveHistory> DBModelList { get; set; }

        public IPagedList<HREmployeeLeaveHistory> DBModelPagedList { get; set; }

        public virtual IEnumerable<proc_GetEmployeeLeaveSummary_Result> DBModelLeaveSummary { get; set; }
        public virtual IEnumerable<proc_GetHREmployeeLeaveHistoryPerYearDateReport_Result> DBModelLeaveBalanceSummary { get; set; }

        public virtual LeaveAppliedSummaryModel LeaveAppliedModel { get; set; }

        public HttpPostedFileBase FormDocument { get; set; }
        public string RecommendationEmployeeName { get; set; }
        public string RecommendationEmployeeDesignation { get; set; }
        public string ApprovalEmployeeName { get; set; }
        public string ApprovalEmployeeDesignation { get; set; }
        public ReportHeaderViewModel Header { get; set; }
    }
    public class HREmployeeLeaveHistoryViewModelList : BreadCrumbModel
    {
        public IPagedList<HREmployeeLeaveHistory> DBModelList { get; set; }
    }
}
