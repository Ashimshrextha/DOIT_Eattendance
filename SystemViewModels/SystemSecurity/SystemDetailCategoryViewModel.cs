using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSecurity;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSecurity
{
    public class SystemDetailCategoryViewModel : BreadCrumbModel
    {
        public virtual SystemDetailCategoryModel DataModel { get; set; }

        public IPagedList<SystemDetailCategoryModel> DataModelList { get; set; }

        public ICollection<SystemDetailCategory> DBModelList { get; set; }


    }
    public class SystemDetailCategoryViewModelList : BreadCrumbModel
    {
        public SystemDetailCategory DBModel { get; set; }

        public IPagedList<SystemDetailCategory> DBModelList { get; set; }
    }
}
