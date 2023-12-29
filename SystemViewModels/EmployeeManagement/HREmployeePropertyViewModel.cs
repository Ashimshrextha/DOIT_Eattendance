using PagedList;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeePropertyViewModel:BreadCrumbModel
    {
        public HREmployeePropertyModel DataModel { get; set; }
        public PagedList<HREmployeePropertyModel> DataModelList { get; set; }
    }
    public class HREmployeePropertyViewModelList : BreadCrumbModel
    {
        public HREmployeeProperty DBModel { get; set; }
        public PagedList<HREmployeeProperty> DBModelList { get; set; }
    }
}
