using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemAuthentication;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeLoginViewModel:BreadCrumbModel
    {
        public MembershipProviderModel DataModelMembership { get; set; }
        public LoginsModel DataModel { get; set; }
        public ICollection<Login> DBModelList { get; set; }
    }
    public class HREmployeeLoginViewModelList : BreadCrumbModel
    {       
        public Login DBModel { get; set; }
        public IPagedList<Login> DBModelList { get; set; }
    }
}
