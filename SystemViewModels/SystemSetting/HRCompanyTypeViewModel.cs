using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HRCompanyTypeViewModel:BreadCrumbModel
    {
        public HRCompanyTypeModel DataModel { get; set; }

        public IPagedList<HRCompanyTypeModel> DataModelList { get; set; }

        public ICollection<HRCompanyType> DBModelList { get; set; }
    }
    public class HRCompanyTypeViewModelList : BreadCrumbModel
    {
        public HRCompanyType DBModel { get; set; }

        public IPagedList<HRCompanyType> DBModelList { get; set; }
    }
}
