using SystemModels.DeviceManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.DeviceManagement
{
    public class DeviceLogsFetcherViewModel:BreadCrumbModel
    {
        public DeviceLogsFetcherModel DataModel { get; set; }
    }
    public class DeviceLogsFetcherViewModelList : BreadCrumbModel
    {
        public DeviceLogsFetcherModel DBModel { get; set; }
    }
}
