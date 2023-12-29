using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SystemInterfaces.Reports
{
    public interface ILeaveReportServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetMonthlyLeaveReport(long? idHRCompany, long? idHREmployee, long? idHRCompanyDivision, int? idJobStatus, long? idEmployeeCategory, DateTime fromDate, DateTime toDate, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "");
        Task<ICollection<TEntity>> GetMonthlyLeaveReport(long? idHRCompany, long? idHREmployee, long? idHRCompanyDivision, int? idJobStatus, long? idEmployeeCategory, DateTime fromDate, DateTime toDate);
    }
}
