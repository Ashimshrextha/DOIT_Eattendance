using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces.Reports
{
    public interface IUserLogsServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "");
    }
}
