using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.EmployeeManagement;
using SystemModels.EmployeeManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.EmployeeManagement
{
    public class KaajAppliedSummaryServices : BaseRepository<LeaveAppliedSummaryModel, proc_GetHREmployeeKaajAppliedSummary_Result>, IKaajAppliedSummaryServices<KaajAppliedSummaryModel>
    {
        public KaajAppliedSummaryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<KaajAppliedSummaryModel> GetAsync(long? idKaajhistory)
        {
            try
            {
                object[] obj =
             {
                    new SqlParameter() {ParameterName = "@paramIdKaajhistory", SqlDbType = SqlDbType.BigInt, Value= idKaajhistory}
            };

                return await ExecuteScalarFunction<KaajAppliedSummaryModel>("EXEC proc_GetHREmployeeKaajAppliedSummary @paramIdKaajhistory", obj);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
