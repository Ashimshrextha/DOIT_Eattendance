using PagedList;
using System.Threading.Tasks;

namespace SystemInterfaces.SystemSecurity
{
    public interface IHREmployeeAttendanceHistoryServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey);
    }
}
