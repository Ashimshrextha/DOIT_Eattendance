using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HRLanguageViewModel:BreadCrumbModel
    {
        public HRLanguageModel DataModel { get; set; }
        public IPagedList<HRLanguageModel> DataModelList { get; set; }
        public ICollection<HRLanguage> DBModelList { get; set; }
    }
    public class HRLanguageViewModelList : BreadCrumbModel
    {
        public HRLanguage DBModel { get; set; }
        public IPagedList<HRLanguage> DBModelList { get; set; }
    }
}
