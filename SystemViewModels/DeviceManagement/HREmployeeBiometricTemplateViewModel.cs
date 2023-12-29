using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.DeviceManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.DeviceManagement
{
    public class HREmployeeBiometricTemplateViewModel : BreadCrumbModel
    {
        public HREmployeeBiometricTemplateModel DataModel { get; set; }
        public PagedList<HREmployeeBiometricTemplateModel> DataModelList { get; set; }
        public ICollection<HREmployeeBiometricTemplate> DBModelList { get; set; }
        public DeviceConnectionModel ConnectionModel { get; set; }
        public DeviceConnectionComponentModel ConnectionComponentModel { get; set; }
    }
    public class HREmployeeBiometricTemplateViewModelList : BreadCrumbModel
    {
        public HREmployeeBiometricTemplate DBModel { get; set; }
        public IPagedList<HREmployeeBiometricTemplate> DBModelList { get; set; }
    }
}
