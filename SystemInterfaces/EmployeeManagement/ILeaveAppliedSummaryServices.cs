using System.Threading.Tasks;

namespace SystemInterfaces.EmployeeManagement
{
    public interface ILeaveAppliedSummaryServices<Tmodel>
    {
        Task<Tmodel> GetAsync(long? idLeavehistory);
    }
}
