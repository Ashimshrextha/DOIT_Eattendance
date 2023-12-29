using PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemDatabase;

namespace SystemInterfaces.EmployeeManagement
{
    public interface IHREmployeeServices<TEntity>
    {
        Task<proc_GetEmployeeDetail_Result> GetEmployee(long? idHRCompany, long? idHREmployee);
        Task<List<proc_GetEmployeeByDesignation_Result>> GetsByCompany(long? idHRCompany, long? idHREmployee, int? idRoleType);
        Task<IPagedList<TEntity>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey);
        Task<IPagedList<TEntity>> GetsByEmployee(long? idEmployee, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");
        Task<IPagedList<TEntity>> GetsByCompany(long? idHRCompany, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");
        Task<bool> CheckUserNameAsync(string UserName);
        Task<IEnumerable<proc_GetEmployeeCountByGender_Result>> GetsEmployeeCountByGender(long? idHRCompany, int? idRoleType);
    }
}
