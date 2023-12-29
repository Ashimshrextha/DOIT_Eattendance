using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSecurity;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSecurity
{
    public class MasterLeaveTitleViewModel : BreadCrumbModel
    {
        public MasterLeaveTitleModel DataModel { get; set; }
        public IPagedList<MasterLeaveTitleModel> DataModelList { get; set; }
        public ICollection<MasterLeaveTitle> DBModelList { get; set; }
    }
    public class MasterLeaveTitleViewModelList : BreadCrumbModel
    {
        public HRCompanyCodeModel DataModel { get; set; }
        public IPagedList<MasterLeaveTitleModel> DataModelList { get; set; }
        public IPagedList<MasterLeaveTitle> DBModelList { get; set; }
    }
}
