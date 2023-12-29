using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSecurity
{
    public class HRLanguageConversionViewModel : BreadCrumbModel
    {
        public HRLanguageConversionModel DataModel { get; set; }

        public IPagedList<HRLanguageConversionModel> DataModelList { get; set; }
        public ICollection<HRLanguageConversion> DBModelList { get; set; }
    }
    public class HRLanguageConversionViewModelList : BreadCrumbModel
    {
        public HRLanguageConversion DBModel { get; set; }

        public IPagedList<HRLanguageConversion> DBModelList { get; set; }
    }
}
