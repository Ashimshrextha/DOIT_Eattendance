using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.DeviceManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.DeviceManagement
{
    public class BiometricTemplateTypeViewModel:BreadCrumbModel
    {
        public BiometricTemplateTypeModel DataModel { get; set; }
        public IPagedList<BiometricTemplateTypeModel> DataModelList { get; set; }
        public ICollection<BiometricTemplateType> DBModelList { get; set; }
    }
    public class BiometricTemplateTypeViewModelList : BreadCrumbModel
    {
        public BiometricTemplateType DBModel { get; set; }
        public IPagedList<BiometricTemplateType> DBModelList { get; set; }
    }
}
