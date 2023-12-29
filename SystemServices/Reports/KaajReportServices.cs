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
using SystemModels.EmployeeManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.Reports
{
    public class KaajReportServices : BaseRepository<HREmployeeKaajHistory, HREmployeeKaajHistoryModel>, IKaajReportServices<HREmployeeKaajHistory>
    {
        public KaajReportServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<HREmployeeKaajHistory>> GetBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var model = await FindAllAsync(x => x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == "");
                return model.OrderBy(orderingBy + " " + orderingDirection)
                 .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IPagedList<proc_GetMonthlyKaajReport_Result>> GetKaajReport(Func<proc_GetMonthlyKaajReport_Result, bool> condition, long? idHRCompany, long? idHREmployee, long? idHRCompanyDivision, int? idJobStatus, DateTime fromDate, DateTime toDate, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value = idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value = idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.DateTime, Value = fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.DateTime, Value = toDate},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey}
            };
                var model = (await UnitOfWork.Db.Database.SqlQuery<proc_GetMonthlyKaajReport_Result>("EXEC proc_GetMonthlyKaajReport  @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramFromDate,@paramToDate,@paramSearch", obj).ToListAsync()).Where(condition);
                return model.OrderBy(orderingBy + " " + orderingDirection)
              .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<proc_GetMonthlyKaajReport_Result>> GetKaajReport(long? idHRCompany, long? idHREmployee, long? idHRCompanyDivision, int? idJobStatus, DateTime fromDate, DateTime toDate)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value = idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value = idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value = fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = toDate},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = ""}
            };
                return await UnitOfWork.Db.Database.SqlQuery<proc_GetMonthlyKaajReport_Result>("EXEC proc_GetMonthlyKaajReport @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramFromDate,@paramToDate,@paramSearch", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
