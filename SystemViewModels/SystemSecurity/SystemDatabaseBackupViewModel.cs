using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSecurity
{
    public class SystemDatabaseBackupViewModel : BreadCrumbModel
    {
        public SystemDatabaseBackupModel DataModel { get; set; }

        public IPagedList<SystemDatabaseBackupModel> DataModeList { get; set; }

        public ICollection<SystemDatabaseBackup> DBModelList { get; set; }
    }
    public class SystemDatabaseBackupViewModelList : BreadCrumbModel
    {
        public SystemDatabaseBackup DBModel { get; set; }

        public IPagedList<SystemDatabaseBackup> DBModelList { get; set; }
    }
}
