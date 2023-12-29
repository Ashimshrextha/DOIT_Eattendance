using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.CompanyManagement
{
    public class HRCompanyDivisionTypeViewModel : BreadCrumbModel
    {
        public HRCompanyDivisionTypeModel DataModel { get; set; }
        public IPagedList<HRCompanyDivisionTypeModel> DataModelList { get; set; }
        public ICollection<HRCompanyDivisionType> DBModelList { get; set; }
    }
    public class HRCompanyDivisionTypeViewModelList : BreadCrumbModel
    {
        public HRCompanyDivisionType DBModel { get; set; }
        public IPagedList<HRCompanyDivisionType> DBModelList { get; set; }
    }
}
