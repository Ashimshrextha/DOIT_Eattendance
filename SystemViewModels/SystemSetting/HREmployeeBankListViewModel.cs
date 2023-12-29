using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HREmployeeBankListViewModel : BreadCrumbModel
    {
        public HREmployeeBankListModel DataModel { get; set; }
        public IPagedList<HREmployeeBankListModel> DataModelList { get; set; }
        public ICollection<HREmployeeBankList> DBModelList { get; set; }
    }
    public class HREmployeeBankListViewModelList : BreadCrumbModel
    {
        public HREmployeeBankList DBModel { get; set; }
        public IPagedList<HREmployeeBankList> DBModelList { get; set; }
    }
}
