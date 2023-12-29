using PagedList;
using System;
using System.Threading.Tasks;
using SystemDatabase;

namespace SystemInterfaces.SystemSecurity
{
    public interface IHREmployeeKaajHistoryServices<TEntity, TModel>
    {
        Task<IPagedList<TEntity>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "");

        Task<bool> CheckKaajTrainingExistance(long? idHREmployee, DateTime fromDate, DateTime toDate);

        Task<proc_GetEmployeeDetail_Result> GetsEmployeeDetail(long? idHREmployee, long? idHRCompany);

    }
}
