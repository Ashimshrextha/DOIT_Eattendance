using PagedList;
using System.Collections.Generic;
using System.Web;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.CompanyManagement
{
    public class HRCompanyViewModel : BreadCrumbModel
    {
        public HRCompanyModel DataModel { get; set; }
        public IPagedList<HRCompanyModel> DataModelList { get; set; }
        public ICollection<HRCompany> DBModelList { get; set; }
        public HRCompany DBModel { get; set; }
        public HttpPostedFileBase HRCompanyLogo { get; set; }
        
    }
    public class HRCompanyViewModelList : BreadCrumbModel
    {
        public HRCompany DBModel { get; set; }
        public IPagedList<HRCompany> DBModelList { get; set; }
    }
}
