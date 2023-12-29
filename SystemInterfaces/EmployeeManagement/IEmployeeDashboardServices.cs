using System.Collections.Generic;
using System.Threading.Tasks;
using SystemDatabase;

namespace SystemInterfaces.EmployeeManagement
{
    public interface IEmployeeDashboardServices
    {
        Task<ICollection<proc_GetTotalEmployeeCountByGender_Result>> GetEmployeeGenderCount(long? idHRCompany, int? idRoleType);
    }
}
