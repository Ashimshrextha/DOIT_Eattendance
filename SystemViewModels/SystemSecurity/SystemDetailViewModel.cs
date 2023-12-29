using PagedList;
using System.Collections.Generic;
using System.Web;
using SystemDatabase;
using SystemModels.SystemSecurity;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSecurity
{
    public class SystemDetailViewModel : BreadCrumbModel
    {
        public virtual SystemDetailModel DataModel { get; set; }
        public IPagedList<SystemDetailModel> DataModelList { get; set; }
        public ICollection<SystemDetail> DBModelList { get; set; }
        public HttpPostedFileBase FileToUpload { get; set; }
    }
    public class SystemDetailViewModelList : BreadCrumbModel
    {
        public SystemDetail DBModel { get; set; }
        public IPagedList<SystemDetail> DBModelList { get; set; }
    }
}
