using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.Feedback;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemFeedback
{
    public class SystemCommentResponseViewModel : BreadCrumbModel
    {
        public SystemCommentResponseModel DataModel { get; set; }
        public IPagedList<SystemCommentResponseModel> DataModelList { get; set; }
        public ICollection<SystemCommentResponse> DBModelList { get; set; }
    }
    public class SystemCommentResponseViewModelList : BreadCrumbModel
    {
        public SystemCommentResponse DBModel { get; set; }
        public IPagedList<SystemCommentResponse> DBModelList { get; set; }
    }
}
