using PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemStores.PairModels;

namespace SystemInterfaces.SystemSecurity
{
    public interface ISystemStructureServices<TEntity> where TEntity : class
    {
        Task<IPagedList<TEntity>> GetBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey);
    }
}
