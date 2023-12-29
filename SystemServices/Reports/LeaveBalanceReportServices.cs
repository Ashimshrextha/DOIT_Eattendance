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
    public class LeaveBalanceReportServices : ILeaveBalanceReportServices<proc_GetBalanceLeaveReport_Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public LeaveBalanceReportServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitofwork");
            }
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<IPagedList<proc_GetBalanceLeaveReport_Result>> GetPagedList(Func<proc_GetBalanceLeaveReport_Result, bool> condition, long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] myObjArray =
           {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramYear", SqlDbType = SqlDbType.Int, Value= year},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= searchKey}
            };
                var model = (await _unitOfWork.Db.Database.SqlQuery<proc_GetBalanceLeaveReport_Result>("EXEC proc_GetBalanceLeaveReport @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramYear,@paramSearch", myObjArray).ToListAsync()).Where(condition);
                return model.OrderBy("HRDesignationRank ASC")
                  .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<proc_GetBalanceLeaveReport_Result>> Gets(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year)
        {
            try
            {
                object[] myObjArray =
           {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramYear", SqlDbType = SqlDbType.Int, Value= year},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= ""}
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_GetBalanceLeaveReport_Result>("EXEC proc_GetBalanceLeaveReport @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramYear,@paramSearch", myObjArray).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<proc_GetAvailableLeavePerYear_Result>> GetsAvailableLeavePerYear(long? idHRCompany, long? idHREmployee, int year)
        {
            try
            {
                object[] myObjArray =
           {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramYear", SqlDbType = SqlDbType.Int, Value= year},
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_GetAvailableLeavePerYear_Result>("EXEC proc_GetAvailableLeavePerYear @paramIdHRCompany,@paramIdHREmployee,@paramYear", myObjArray).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public virtual async Task<ICollection<proc_GetHREmployeeLeaveHistoryPerYearReport_Result>> GetsLeavePerYearReport(long? idHREmployee, int year)
        {
            try
            {
                object[] myObjArray =
           {
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramYear", SqlDbType = SqlDbType.Int, Value= year},
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_GetHREmployeeLeaveHistoryPerYearReport_Result>("EXEC proc_GetHREmployeeLeaveHistoryPerYearReport @paramIdHREmployee,@paramYear", myObjArray).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
