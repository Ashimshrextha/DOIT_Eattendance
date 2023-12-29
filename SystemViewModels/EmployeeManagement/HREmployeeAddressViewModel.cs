using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeAddressViewModel : BreadCrumbModel
    {
        public HREmployeeAddressModel DataModel { get; set; }

        public ICollection<HREmployeeAddress> DBModelList { get; set; }
    }
    public class HREmployeeAddressViewModelList : BreadCrumbModel
    {
        public HREmployeeAddress DBModel { get; set; }

        public IPagedList<HREmployeeAddress> DBModelList { get; set; }
    }
}
