using PagedList;
using System.Threading.Tasks;

namespace SystemInterfaces.CompanyManagement
{
    public interface IHRCompanyServices<TEntity> where TEntity : class
    {
        Task<IPagedList<TEntity>> GetsBySearchKey(long? idHRCompany, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey);
        Task<IPagedList<TEntity>> GetsChildCompany(long? idCompany, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");
    }
}
