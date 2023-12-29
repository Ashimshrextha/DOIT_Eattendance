using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HREmployeeBloodGroupViewModel : BreadCrumbModel
    {
        public HREmployeeBloodGroupModel DataModel { get; set; }
        public IPagedList<HREmployeeBloodGroupModel> DataModelList { get; set; }
        public ICollection<HREmployeeBloodGroup> DBModelList { get; set; }
    }
    public class HREmployeeBloodGroupViewModelList : BreadCrumbModel
    {
        public HREmployeeBloodGroup DBModel { get; set; }
        public IPagedList<HREmployeeBloodGroup> DBModelList { get; set; }
    }
}
