using PagedList;
using System.Collections.Generic;
using System.Web;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeKaajHistoryViewModel : BreadCrumbModel
    {
        public virtual KaajAppliedSummaryModel KaajModel { get; set; }
        public virtual HREmployeeKaajHistoryModel DataModel { get; set; }
        public virtual proc_GetEmployeeDetail_Result EmployeeDetail { get; set; }
        public ICollection<HREmployeeKaajHistory> DBModelList { get; set; }
        public IPagedList<HREmployeeKaajHistory> DBModelPagedList { get; set; }
        public IEnumerable<string> ddlKajVehicalSelected { get; set; }
        //public HttpPostedFileWrapper FormDocument { get; set; }
        //Modiy 2020-05-01 Prem Prakash Dhakal
        public HttpPostedFileBase FormDocument { get; set; }
        public int lastYearNp { get; set; }
        public string CurrentDesignation { get; set; }
        public string ApprovedBy { get; set; }
        public ReportHeaderViewModel Header { get; set; }
    }
    public class HREmployeeKaajHistoryViewModelList : BreadCrumbModel
    {
        public HREmployeeKaajHistory DBModel { get; set; }
        public IPagedList<HREmployeeKaajHistory> DBModelList { get; set; }
        public int lastYearNp { get; set; }
        public string CurrentDesignation { get; set; }


    }
}
