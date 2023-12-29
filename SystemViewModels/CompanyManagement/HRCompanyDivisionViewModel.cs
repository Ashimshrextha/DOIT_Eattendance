using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.CompanyManagement
{
    public class HRCompanyDivisionViewModel:BreadCrumbModel
    {
        public HRCompanyDivisionModel DataModel { get; set; }
        public IPagedList<HRCompanyDivisionModel> DataModelList { get; set; }
        public ICollection<HRCompanyDivision> DBModelList { get; set; }
    }
    public class HRCompanyDivisionViewModelList : BreadCrumbModel
    {
        public HRCompanyDivision DBModel { get; set; }
        public IPagedList<HRCompanyDivision> DBModelList { get; set; }
    }
}
