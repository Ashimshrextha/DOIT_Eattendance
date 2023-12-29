using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDatabase;

namespace SystemInterfaces.SystemSecurity
{
    public interface IHREmployeeLeaveHistoryServices <TEntity,TModel> 
    {
        Task<proc_GetEmployeeDetail_Result> GetsEmployeeDetail(long? idHREmployee, long? idHRCompany);
    }
}
