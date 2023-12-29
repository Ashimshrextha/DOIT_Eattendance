using PagedList;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeNationalIdentityViewModel:BreadCrumbModel
    {
        public HREmployeeNationalIdentityModel DataModel { get; set; }

        public PagedList<HREmployeeNationalIdentityModel> DataModelList { get; set; }
    }
    public class HREmployeeNationalIdentityViewModelList : BreadCrumbModel
    {
        public HREmployeeNationalIdentity DBModel { get; set; }

        public PagedList<HREmployeeNationalIdentity> DBModelList { get; set; }
    }
}
