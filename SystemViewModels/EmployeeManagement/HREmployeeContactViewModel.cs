using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeContactViewModel : BreadCrumbModel
    {
        public HREmployeeContactModel DataModel { get; set; }
        public IPagedList<HREmployeeContactModel> DataModelList { get; set; }
		public ICollection<HREmployeeContact> DBModelList { get; set; }
	}
    public class HREmployeeContactViewModelList : BreadCrumbModel
    {
        public HREmployeeContact DBModel { get; set; }
        public IPagedList<HREmployeeContact> DBModelList { get; set; }
	}
}
