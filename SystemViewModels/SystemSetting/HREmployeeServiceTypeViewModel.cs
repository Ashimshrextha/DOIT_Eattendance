using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HREmployeeServiceTypeViewModel : BreadCrumbModel
    {
        public HREmployeeServiceTypeModel DataModel { get; set; }
        public IPagedList<HREmployeeServiceTypeModel> DataModelList { get; set; }
        public ICollection<HREmployeeServiceType> DBModelList { get; set; }
    }
    public class HREmployeeServiceTypeViewModelList : BreadCrumbModel
    {
        public HREmployeeServiceType DBModel { get; set; }
        public IPagedList<HREmployeeServiceType> DBModelList { get; set; }
    }
}
