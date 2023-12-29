using PagedList;
using System.Threading.Tasks;
using SystemDatabase;

namespace SystemInterfaces.SystemSetting
{
    public interface IHREmployeeCategoryServices
    {
        Task<IPagedList<HREmployeeCategory>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey);
        Task<IPagedList<HREmployeeCategory>> GetsBySearchKey(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection);
    }
}
