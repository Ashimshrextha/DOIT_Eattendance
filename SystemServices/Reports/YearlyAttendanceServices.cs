using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.Reports;
using SystemUnitOfWork.Interfaces;

namespace SystemServices.Reports
{
    public class YearlyAttendanceServices : IYearlyAttendanceServices<proc_GetYearMonthlyAttendanceReport_Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public YearlyAttendanceServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("UnitOfWork");
            }
            this._unitOfWork = unitOfWork;
        }

        public virtual async Task<ICollection<proc_GetYearMonthlyAttendanceReport_Result>> Gets(long? idHREmployee, int? idJobStatus, int yearNP)
        {
            try
            {
                object[] param =
            {
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramYearNP", SqlDbType = SqlDbType.Int, Value= yearNP}
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_GetYearMonthlyAttendanceReport_Result>("EXEC proc_GetYearMonthlyAttendanceReport @paramIdHREmployee,@paramIdJobStatus,@paramYearNP", param).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
