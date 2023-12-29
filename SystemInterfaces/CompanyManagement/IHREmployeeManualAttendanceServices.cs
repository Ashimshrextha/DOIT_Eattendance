using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SystemInterfaces.CompanyManagement
{
   public interface IHREmployeeManualAttendanceServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey);
        Task<IPagedList<TEntity>> GetsByFileName(long? idEmployee, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");
    }
}
