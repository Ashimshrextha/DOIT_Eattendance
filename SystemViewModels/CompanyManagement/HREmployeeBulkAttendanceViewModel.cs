using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;
namespace SystemViewModels.CompanyManagement
{
   public class HREmployeeBulkAttendanceViewModel : BreadCrumbModel
    {
        public HREmployeeManualAttendanceModel DataModel { get; set; }
        public IPagedList<HREmployeeManualAttendanceModel> DataModelList { get; set; }
        public ICollection<HREmployeeManualAttendance> DBModelList { get; set; }
        public HttpPostedFileBase FileToUpload { get; set; }
    }
    public class HREmployeeBulkAttendanceViewModelList : BreadCrumbModel
    {
        public HREmployeeManualAttendance DBModel { get; set; }
        public IPagedList<HREmployeeManualAttendance> DBModelList { get; set; }
        public IPagedList<proc_GetEmployeeBulkAttendance_Result> DBModelManualList { get; set; }
    }
}
