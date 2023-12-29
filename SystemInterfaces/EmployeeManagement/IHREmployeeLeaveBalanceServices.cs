using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces.EmployeeManagement
{
    public interface IHREmployeeLeaveBalanceServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetsBySearchKey(long? idHREmployee,int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey="");
    }
}
