using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemModels.DeviceManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.CompanyManagement
{
    public  class HRCompanyAttendanceViewModel: BreadCrumbModel
    {

        public HRCompanyManualAttendanceModel DataModel { get; set; }
        public IPagedList<HRCompanyManualAttendanceModel> DataModelList { get; set; }
        public ICollection<HRCompanyManualAttendance> DBModelList { get; set; }
        public HttpPostedFileBase FileToUpload { get; set; }
        public HttpPostedFileBase FileToUploadGoverment { get; set; }

        public string files { get; set; }


    }
    public class HRCompanyAttendanceViewModellist : BreadCrumbModel
        {
        public HRCompanyManualAttendance DBModel { get; set; }
        public IPagedList<HRCompanyManualAttendance> DBModelList { get; set; }

    }


}
