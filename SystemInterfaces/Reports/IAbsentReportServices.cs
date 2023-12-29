using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SystemInterfaces.Reports
{
    public interface IAbsentReportServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetResult(long? IdHRCompany, long? HRCompanyDivision, int? idJobStatus, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");
        Task<ICollection<TEntity>> GetResult(long? IdHRCompany, long? HRCompanyDivision, int? idJobStatus, DateTime date);

    }
}
