using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class SystemPermissionByRoleViewModel : BreadCrumbModel
    {
        public List<SystemPermissionByRoleModel> DataModel { get; set; }
        public IEnumerable<proc_RoleMenuSetupList_Result> DataModelSystemStructure { get; set; }
    }
    public class SystemPermissionByRoleViewModelList : BreadCrumbModel
    {
        public HREmployeeRole DBModel { get; set; }
        public IPagedList<HREmployeeRole> DBModelList { get; set; }
    }
}
