using PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SystemInterfaces.SystemSetting
{
	public interface ISystemPermissionByRoleServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey);
		Task<IEnumerable<T>> RoleMenuSetupList<T>(int? idRole, long? idHREmployee);
	}
}
