using System;
using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class DashboardViewModel : BreadCrumbModel
    {
    }
    public class DashboardViewModelList : BreadCrumbModel
    {
        public ICollection<proc_GetTotalEmployeeCountByGender_Result> DBGenderModel { get; set; }
       
        public IEnumerable<proc_MonthlyCalendar_Result> DBModelCalendar { get; set; }

        public string TodayDayNp { get; set; }
    }
}
