using PagedList;
using System;
using System.Collections.Generic;

namespace SystemInterfaces.Reports
{
    public interface IWorkedReportServices<TEntity>
    {
        IPagedList<TEntity> GetPagedList(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");
        ICollection<TEntity> Gets(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date);
    }
}
