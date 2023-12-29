using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSecurity;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSecurity
{
    public class HRCalendarViewModel : BreadCrumbModel
    {
        public HRCalendarModel DataModel { get; set; }
        public IPagedList<HRCalendarModel> DataModelList { get; set; }

        public ICollection<HRCalendar> DBModelList { get; set; }
    }
    public class HRCalendarViewModelList : BreadCrumbModel
    {
        public HRCalendar DBModel { get; set; }

        public IPagedList<HRCalendar> DBModelList { get; set; }
    }
}
