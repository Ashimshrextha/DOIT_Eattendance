using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.CompanyManagement
{
    public class HRCompanySystemSupportViewModel : BreadCrumbModel
    {
        public HRCompanySystemSupportModel DataModel { get; set; }
        public IPagedList<HRCompanySystemSupportModel> DataModelList { get; set; }
        public ICollection<HRCompanySystemSupport> DBModelList { get; set; }
    }
    public class HRCompanySystemSupportViewModelList : BreadCrumbModel
    {
        public HRCompanySystemSupport DBModel { get; set; }
        public IPagedList<HRCompanySystemSupport> DBModelList { get; set; }
    }
}
