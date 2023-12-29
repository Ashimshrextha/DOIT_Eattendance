using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.Feedback;
using SystemStores.GlobalModels;
using static SystemStores.ENUMData.EnumGlobal;

namespace SystemViewModels.SystemFeedback
{
    public class SystemCommentViewModel : BreadCrumbModel
    {
        public SystemCommentModel DataModel { get; set; }
        public Priority PriorityEnum { get; set; }
        public IPagedList<SystemCommentModel> DataModelList { get; set; }
        public ICollection<SystemComment> DBModelList { get; set; }
    }
    public class SystemCommentViewModelList : BreadCrumbModel
    {
        public SystemComment DBModel { get; set; }
        public Priority PriorityEnum { get; set; }
        public IPagedList<SystemComment> DBModelList { get; set; }
    }
}
