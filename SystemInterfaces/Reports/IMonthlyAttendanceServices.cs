using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SystemInterfaces.Reports
{
    public interface IMonthlyAttendanceServices<TEntity>
    {
        Task<ICollection<TEntity>> GetResult(long? IdHRCompany, long? HRCompanyDivision, int? idJobStatus,long? IdHREemployee, DateTime FromDate, DateTime ToDate);
        Task<IPagedList<TEntity>> GetResult(Func<TEntity, bool> condition, long? IdHRCompany, long? HRCompanyDivision, int? idJobStatus,long? IdHREemployee, DateTime FromDate, DateTime ToDate, int pageNumber, int pageSize, string orderingBy, string orderingDirection,string searchKey="");
    }
}
