using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.CompanyManagement
{
    public class HRCompanyNotificationViewModel : BreadCrumbModel
    {
        public HRCompanyNotificationModel DataModel { get; set; }
        public IPagedList<HRCompanyNotificationModel> DataModelList { get; set; }
        public ICollection<HRCompanyNotification> DBModelList { get; set; }
    }
    public class HRCompanyNotificationViewModelList : BreadCrumbModel
    {
        public HRCompanyNotification DBModel { get; set; }
        public IPagedList<HRCompanyNotification> DBModelList { get; set; }
    }
}
