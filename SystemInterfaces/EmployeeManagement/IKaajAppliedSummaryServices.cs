using System.Threading.Tasks;

namespace SystemInterfaces.EmployeeManagement
{
    public interface IKaajAppliedSummaryServices<Tmodel>
    {
        Task<Tmodel> GetAsync(long? idKaajhistory);
    }
}
