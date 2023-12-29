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
    public class LeaveAppliedSummaryServices : BaseRepository<LeaveAppliedSummaryModel, proc_GetHREmployeeLeaveAppliedSummary_Result>, ILeaveAppliedSummaryServices<LeaveAppliedSummaryModel>
    {
        public LeaveAppliedSummaryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<LeaveAppliedSummaryModel> GetAsync(long? idLeavehistory)
        {
            try
            {
                object[] obj =
             {
                    new SqlParameter() {ParameterName = "@paramIdLeavehistory", SqlDbType = SqlDbType.BigInt, Value= idLeavehistory}
            };

                return await ExecuteScalarFunction<LeaveAppliedSummaryModel>("EXEC proc_GetHREmployeeLeaveAppliedSummary @paramIdLeavehistory", obj);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
