using PagedList;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeBankAccountViewModel:BreadCrumbModel
    {
        public HREmployeeBankAccountModel DataModel { get; set; }

        public PagedList<HREmployeeBankAccountModel> DataModelList { get; set; }
    }
    public class HREmployeeBankAccountViewModelList : BreadCrumbModel
    {
        public HREmployeeBankAccount DBModel { get; set; }

        public PagedList<HREmployeeBankAccount> DBModelList { get; set; }
    }
}
