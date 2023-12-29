using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.Reports;
using SystemUnitOfWork.Interfaces;

namespace SystemServices.Reports
{
    public class LeaveReportServices : ILeaveReportServices<proc_GetMonthlyLeaveReport_Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaveReportServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitofwork");
            }
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<IPagedList<proc_GetMonthlyLeaveReport_Result>> GetMonthlyLeaveReport(long? idHRCompany, long? idHREmployee, long? idHRCompanyDivision, int? idJobStatus, long? idEmployeeCategory, DateTime fromDate, DateTime toDate, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] obj =
               {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value = idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value = idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdEmployeeCategory", SqlDbType = SqlDbType.BigInt, Value = idEmployeeCategory},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.DateTime, Value = fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.DateTime, Value = toDate},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey}
               };
                return (await _unitOfWork.Db.Database.SqlQuery<proc_GetMonthlyLeaveReport_Result>("EXEC proc_GetMonthlyLeaveReport @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramIdEmployeeCategory,@paramFromDate,@paramToDate,@paramSearch", obj).ToListAsync()).OrderBy(orderingBy + " " + orderingDirection).ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }
        public virtual async Task<ICollection<proc_GetMonthlyLeaveReport_Result>> GetMonthlyLeaveReport(long? idHRCompany, long? idHREmployee, long? idHRCompanyDivision, int? idJobStatus, long? idEmployeeCategory, DateTime fromDate, DateTime toDate)
        {
            try
            {
                object[] obj =
               {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value = idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value = idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdEmployeeCategory", SqlDbType = SqlDbType.BigInt, Value = idEmployeeCategory},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.DateTime, Value = fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.DateTime, Value = toDate},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = ""}
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_GetMonthlyLeaveReport_Result>("EXEC proc_GetMonthlyLeaveReport  @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramIdEmployeeCategory,@paramFromDate,@paramToDate,@paramSearch", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }
    }
}
