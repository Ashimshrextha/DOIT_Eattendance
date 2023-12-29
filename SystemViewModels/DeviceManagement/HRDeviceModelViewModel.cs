using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.DeviceManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.DeviceManagement
{
    public class HRDeviceModelViewModel : BreadCrumbModel
    {
        public HRDeviceModels DataModel { get; set; }
        public IPagedList<HRDeviceModel> DataModelList { get; set; }
        public ICollection<HRDeviceModel> DBModelList { get; set; }
    }
    public class HRDeviceModelViewModelList : BreadCrumbModel
    {
        public HRDeviceModel DBModel { get; set; }
        public IPagedList<HRDeviceModel> DBModelList { get; set; }
    }
}
