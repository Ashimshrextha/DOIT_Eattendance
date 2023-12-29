using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HREmployeeContactTypeViewModel: BreadCrumbModel
    {
        public HREmployeeContactTypeModel DataModel { get; set; }
        public IPagedList<HREmployeeContactTypeModel> DataModelList { get; set; }
        public ICollection<HREmployeeContactType> DBModelList { get; set; }
    }
    public class HREmployeeContactTypeViewModelList : BreadCrumbModel
    {
        public HREmployeeContact DBModel { get; set; }

        public IPagedList<HREmployeeContactType> DBModelList { get; set; }
    }
}
