using PagedList;
using System.Threading.Tasks;

namespace SystemInterfaces.CompanyManagement
{
    public interface IHRCompanyNotificationServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey);
    }
}
