using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSecurity;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSecurity
{
    public class SystemMenuViewModel:BreadCrumbModel
    {
        public virtual SystemMenuModel DataModel { get; set; }
        public virtual ICollection<SystemMenuModel> DataModelList { get; set; }
        public virtual ICollection<SystemMenu> DBModelList { get; set; }
    }
    public class SystemMenuViewModelList : BreadCrumbModel
    {
        public virtual SystemMenu DBModel { get; set; }
        public virtual IPagedList<SystemMenu> DBModelList { get; set; }
    }
}
