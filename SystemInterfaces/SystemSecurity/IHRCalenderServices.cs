using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemDatabase;

namespace SystemInterfaces.SystemSecurity
{
    public interface IHRCalendarServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetBySearchKey(int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey);
        Task<ICollection<proc_GetMonthlyAttendanceHeader_Result>> GetMonthlyAttendanceHeader(int year, int month);
        Task<ICollection<proc_MonthlyCalendar_Result>> GetCalendar(long? idHREmployee, DateTime fromDate, DateTime toDate);
    }
}
