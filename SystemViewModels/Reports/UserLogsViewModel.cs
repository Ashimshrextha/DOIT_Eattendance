using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.Reports;
using SystemStores.GlobalModels;

namespace SystemViewModels.Reports
{
    public class UserLogsViewModel : BreadCrumbModel
    {
        public UserLogsModel DataModel { get; set; }

        public IPagedList<UserLogsModel> DataModelList { get; set; }

        public ICollection<UserLog> DBModelList { get; set; }
    }
    public class UserLogsViewModelList : BreadCrumbModel
    {
        public UserLog DBModel { get; set; }

        public IPagedList<UserLog> DBModelList { get; set; }
    }
}
