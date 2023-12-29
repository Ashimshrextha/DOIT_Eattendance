using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HREmployeeMaritalViewModel:BreadCrumbModel
    {
        public HREmployeeMaritalModel DataModel { get; set; }
        public IPagedList<HREmployeeMaritalModel> DataModelList { get; set; }
        public ICollection<HREmployeeMarital> DBModelList { get; set; }
    }
    public class HREmployeeMaritalViewModelList : BreadCrumbModel
    {
        public HREmployeeMarital DBModel { get; set; }
        public IPagedList<HREmployeeMarital> DBModelList { get; set; }
    }
}
