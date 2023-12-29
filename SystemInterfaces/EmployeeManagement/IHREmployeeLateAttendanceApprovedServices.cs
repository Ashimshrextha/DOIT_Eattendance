using PagedList;
using System.Threading.Tasks;

namespace SystemInterfaces.EmployeeManagement
{
    public interface IHREmployeeLateAttendanceApprovedServices<TEntity>
    {
		Task<IPagedList<TEntity>> GetsByEmployee(long? idEmployee, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");

	}
}
