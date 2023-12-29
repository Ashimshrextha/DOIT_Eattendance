using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;

namespace SystemViewModels.CompanyManagement
{
    public class DashboardViewModel:BreadCrumbModel
    {
    }
    public class DashboardViewModelList : BreadCrumbModel
    {
        public ICollection<proc_GetTotalEmployeeCountByGender_Result> DBGenderModel { get; set; }
    }
}
