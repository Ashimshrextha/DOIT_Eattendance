using PagedList;
using System.Threading.Tasks;

namespace SystemInterfaces.DeviceManagement
{
    public interface IBiometricTemplateTypeServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey);
    }
}
