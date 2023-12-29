using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HRCompanyLeaveTypeViewModel:BreadCrumbModel
    {
        public HRCompanyLeaveTypeModel DataModel { get; set; }
        public IPagedList<HRCompanyLeaveTypeModel> DataModelList { get; set; }
        public ICollection<HRCompanyLeaveType> DBModelList { get; set; }
    }
    public class HRCompanyLeaveTypeViewModelList : BreadCrumbModel
    {
        public HRCompanyLeaveType DBModel { get; set; }
        public IPagedList<HRCompanyLeaveType> DBModelList { get; set; }
    }
}
