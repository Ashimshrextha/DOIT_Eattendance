using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.CompanyManagement
{
    public class HRDeviceViewModel:BreadCrumbModel
    {
        public HRDevicesModel DataModel { get; set; }

        public IPagedList<HRDevicesModel> DataModelList { get; set; }
        public ICollection<HRDevice> DBModelList { get; set; }
    }
    public class HRDeviceViewModelList : BreadCrumbModel
    {
        public HRDevice DBModel { get; set; }

        public IPagedList<HRDevice> DBModelList { get; set; }
    }
}
