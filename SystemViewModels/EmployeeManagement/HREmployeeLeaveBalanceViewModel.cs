using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeLeaveBalanceViewModel:BreadCrumbModel
    {
        public HREmployeeLeaveBalanceModel DataModel { get; set; }

        public ICollection<HREmployeeLeaveBalance> DBModelList { get; set; }
        public int lastYearNp { get; set; }
    }
    public class HREmployeeLeaveBalanceViewModelList : BreadCrumbModel
    {
        public HREmployeeLeaveBalance DBModel { get; set; }

        public IPagedList<HREmployeeLeaveBalance> DBModelList { get; set; }
        public int lastYearNp { get; set; }
    }
}
