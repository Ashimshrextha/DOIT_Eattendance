using PagedList;
using SystemDatabase;
using SystemStores.GlobalModels;

namespace SystemViewModels.Reports
{
    public class CurrentYearLeaveSummaryViewModel : BreadCrumbModel
    {
    }
    public class CurrentYearLeaveSummaryViewModelList : BreadCrumbModel
    {
        public IPagedList<HREmployee> DBModelList { get; set; }
    }
}
