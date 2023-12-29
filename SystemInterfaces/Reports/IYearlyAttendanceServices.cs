using System.Collections.Generic;
using System.Threading.Tasks;

namespace SystemInterfaces.Reports
{
    public interface IYearlyAttendanceServices<Tentity>
    {
        Task<ICollection<Tentity>> Gets(long? idHREmployee, int? idJobStatus,int yearNP);
    }
}
