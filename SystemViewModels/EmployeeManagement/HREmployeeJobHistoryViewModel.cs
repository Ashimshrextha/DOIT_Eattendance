using PagedList;
using System.Collections.Generic;
using System.Web;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeJobHistoryViewModel : BreadCrumbModel
    {
        //public HttpPostedFileWrapper DocumentUpload { get; set; }
        //Modiy 2020-05-01 Prem Prakash Dhakal
        public HttpPostedFileBase DocumentUpload { get; set; }
        public HREmployeeJobHistoryModel DataModel { get; set; }
        public IPagedList<HREmployeeJobHistoryModel> DataModelList { get; set; }
        public ICollection<HREmployeeJobHistory> DBModelList { get; set; }
    }

    public class HREmployeeJobHistoryViewModelList : BreadCrumbModel
    {
        public int JobHistoryCounter { get; set; }
        public HREmployeeJobHistory DBModel { get; set; }
        public IPagedList<HREmployeeJobHistory> DBModelList { get; set; }
    }
}
