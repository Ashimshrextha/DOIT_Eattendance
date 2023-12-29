using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSecurity;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSecurity
{
    public class SystemStructureViewModel : BreadCrumbModel
    {
        public virtual SystemStructureModel DataModel { get; set; }

        public IPagedList<SystemStructureModel> DataModelList { get; set; }

        public ICollection<SystemStructure> DBModelList { get; set; }


    }
    public class SystemStructureViewModelList : BreadCrumbModel
    {
        public SystemStructure DBModel { get; set; }

        public IPagedList<SystemStructure> DBModelList { get; set; }
    }
}
