using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HREmployeeJobStatusViewModel:BreadCrumbModel
    {
        public HREmployeeJobStatusModel DataModel { get; set; }

        public IPagedList<HREmployeeJobStatusModel> DataModelList { get; set; }

        public ICollection<HREmployeeJobStatu> DBModelList { get; set; }
    }
    public class HREmployeeJobStatusViewModelList : BreadCrumbModel
    {
        public HREmployeeJobStatu DBModel { get; set; }

        public IPagedList<HREmployeeJobStatu> DBModelList { get; set; }
    }
}
