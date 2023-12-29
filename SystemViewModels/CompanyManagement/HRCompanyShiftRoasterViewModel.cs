using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.CompanyManagement
{
    public class HRCompanyShiftRoasterViewModel:BreadCrumbModel
    {
        public HRCompanyShiftRoasterModel DataModel { get; set; }
        public ICollection<HRCompanyShiftRoaster> DBModelList { get; set; }
    }
    public class HRCompanyShiftRoasterViewModelList : BreadCrumbModel
    {
        public IPagedList<HRCompanyShiftRoaster> DBModelList { get; set; }
    }
}
