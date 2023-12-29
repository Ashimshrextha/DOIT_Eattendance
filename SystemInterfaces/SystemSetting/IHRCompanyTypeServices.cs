using PagedList;
using System.Threading.Tasks;

namespace SystemInterfaces.SystemSetting
{
    public interface IHRCompanyTypeServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey);
    }
}
