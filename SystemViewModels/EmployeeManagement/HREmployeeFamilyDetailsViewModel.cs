using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeFamilyDetailsViewModel : BreadCrumbModel
    {
        public HREmployeeModel DataModel { get; set; }
        public IPagedList<HREmployeeModel> DataModelList { get; set; }
        public ICollection<HREmployee> DBModelList { get; set; }
    }

    public class HREmployeeFamilyDetailsViewModelList : BreadCrumbModel
    {
        public HREmployee DBModel { get; set; }
        public IPagedList<HREmployee> DBModelList { get; set; }
    }
}
