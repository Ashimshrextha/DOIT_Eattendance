using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class SystemPermissionByHREmployeeViewModel : BreadCrumbModel
    {
		public List<SystemPermissionByHREmployeeModel> DataModel { get; set; }
		public IEnumerable<proc_EmployeeMenuSetupList_Result> DataModelSystemStructure { get; set; }
	}
    public class SystemPermissionByHREmployeeViewModelList : BreadCrumbModel
    {
		public HREmployee DBModel { get; set; }

		public IPagedList<HREmployee> DBModelList { get; set; }
	}
}
