using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SystemInterfaces.Reports
{
    public interface IMonthlyAttendanceSummaryServices<TEntity>
    {
        Task<ICollection<TEntity>> Gets(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime fromDate, DateTime toDate);
        Task<IPagedList<TEntity>> GetPagedList(Func<TEntity, bool> condition, long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime fromDate, DateTime toDate, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "");
    }
}
