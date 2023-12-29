using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemDatabase;

namespace SystemInterfaces.Reports
{
    public interface ILeaveBalanceReportServices<TEntity>
    {
        Task<IPagedList<proc_GetBalanceLeaveReport_Result>> GetPagedList(Func<TEntity, bool> condition, long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus,long? idHREmployee, int year, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "");
        Task<ICollection<proc_GetBalanceLeaveReport_Result>> Gets(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus,long? idHREmployee, int year);
        Task<ICollection<proc_GetAvailableLeavePerYear_Result>> GetsAvailableLeavePerYear(long? idHRCompany, long? idHREmployee, int year);
        Task<ICollection<proc_GetHREmployeeLeaveHistoryPerYearReport_Result>> GetsLeavePerYearReport(long? idHREmployee, int year);
    }
}
