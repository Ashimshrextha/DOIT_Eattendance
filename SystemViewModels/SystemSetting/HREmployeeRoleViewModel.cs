using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HREmployeeRoleViewModel:BreadCrumbModel
    {
        public HREmployeeRoleModel DataModel { get; set; }
        public IPagedList<HREmployeeRoleModel> DataModelList { get; set; }
        public ICollection<HREmployeeRole> DBModelList { get; set; }
    }
    public class HREmployeeRoleViewModelList : BreadCrumbModel
    {
        public HREmployeeRole DBModel { get; set; }
        public IPagedList<HREmployeeRole> DBModelList { get; set; }
    }
}
