using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeLeaveSummaryViewModel : BreadCrumbModel
	{
		public HREmployeeLeaveHistoryModel DataModel { get; set; }
		public IPagedList<HREmployeeLeaveHistoryModel> DataModelList { get; set; }
		public ICollection<HREmployeeLeaveHistory> DBModelList { get; set; }
        public string CurrentDesignation { get; set; }
        public int lastYearNp { get; set; }
    }
	public class HREmployeeLeaveSummaryViewModelList : BreadCrumbModel
	{
		public HREmployeeLeaveHistory DBModel { get; set; }
		public IPagedList<HREmployeeLeaveHistory> DBModelList { get; set; }
        public string CurrentDesignation { get; set; }
        public int lastYearNp { get; set; }


    }
}
