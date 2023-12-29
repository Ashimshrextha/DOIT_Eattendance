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
    public  class HRCompanyOfficeLogsViewModel: BreadCrumbModel
    {

        public HRCompanyOfficeLogsModel DataModel { get; set; }
        public IPagedList<HRCompanyOfficeLogsModel> DataModelList { get; set; }
        public ICollection<HRCompanyEmployeeOfficeLog> DBModelList { get; set; }
        public HttpPostedFileBase FileToUpload { get; set; }
        public HttpPostedFileBase FileToUploadGoverment { get; set; }

        public string files { get; set; }


    }
    public class HRCompanyOfficeLogsViewModellist : BreadCrumbModel
        {
        public HRCompanyEmployeeOfficeLog DBModel { get; set; }
        public IPagedList<HRCompanyEmployeeOfficeLog> DBModelList { get; set; }

    }


}
