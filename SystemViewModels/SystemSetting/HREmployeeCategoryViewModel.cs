using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HREmployeeCategoryViewModel : BreadCrumbModel
    {
        public HREmployeeCategoryModel DataModel { get; set; }

        public IPagedList<HREmployeeCategoryModel> DataModelList { get; set; }
        public ICollection<HREmployeeCategory> DBModelList { get; set; }
    }
    public class HREmployeeCategoryViewModelList : BreadCrumbModel
    {
        public HREmployeeCategory DBModel { get; set; }

        public IPagedList<HREmployeeCategory> DBModelList { get; set; }
    }
}
