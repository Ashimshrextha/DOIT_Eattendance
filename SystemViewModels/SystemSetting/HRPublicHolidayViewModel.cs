using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HRPublicHolidayViewModel:BreadCrumbModel
    {
        public HRPublicHolidayModel DataModel { get; set; }
        public IPagedList<HRPublicHolidayModel> DataModelList { get; set; }
        public ICollection<HRPublicHoliday> DBModelList { get; set; }

      
    }
    public class HRPublicHolidayViewModelList : BreadCrumbModel
    {
        public HRPublicHoliday DBModel { get; set; }
        public IPagedList<HRPublicHoliday> DBModelList { get; set; }
    }
}
