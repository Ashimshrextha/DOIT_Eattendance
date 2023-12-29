using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces.Feedback
{
    public interface ISystemCommentResponseServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey);
        Task<IPagedList<TEntity>> GetsByEmployee(long? idEmployee, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");
    }
}
