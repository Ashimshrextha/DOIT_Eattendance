using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSecurity;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSecurity
{
    public class HRCompanyCodeViewModel:BreadCrumbModel
    {
        public HRCompanyCodeModel DataModel { get; set; }
        public IPagedList<HRCompanyCodeModel> DataModelList { get; set; }
        public ICollection<HRCompanyCode> DBModelList { get; set; }
    }
    public class HRCompanyCodeViewModelList : BreadCrumbModel
    {
        public HRCompanyCode DBModel { get; set; }

        public IPagedList<HRCompanyCode> DBModelList { get; set; }
    }
}
