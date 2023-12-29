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
using SystemModels.Reports;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.Reports
{
    public class EarlyCheckoutAttendanceReportServices: BaseRepository<proc_EarlyCheckoutReport_Result, EarlyCheckoutAttendanceReportModel>, IEarlyCheckoutAttendanceReportServices
    {
        public EarlyCheckoutAttendanceReportServices(IUnitOfWork unitOfWork):base(unitOfWork)
        {

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

        }

        public virtual async Task<proc_GetEmployeeDetail_Result> GetEmployeeDetail(long? idHREmployee, long? idHRCompany)
        {
            try
            {
                object[] param =
          {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.Int, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.Int, Value= idHREmployee}
            };
                return await UnitOfWork.Db.Database.SqlQuery<proc_GetEmployeeDetail_Result>("EXEC proc_GetEmployeeDetail @paramIdHREmployee,@paramIdHRCompany", param).FirstOrDefaultAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IPagedList<proc_EarlyCheckoutReport_Result>> GetPagedList(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision??0},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??0},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.BigInt, Value= idHREmployee??0},
                new SqlParameter() {ParameterName = "@p4", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= searchKey}
            };
                var model = await UnitOfWork.Db.Database.SqlQuery<proc_EarlyCheckoutReport_Result>("Exec proc_EarlyCheckoutReport @p1,@p2,@paramIdJobStatus,@p3,@p4,@paramSearch", myObjArray).ToListAsync();
                return model.OrderBy("HRDesignationOrder ASC")
                    .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<proc_EarlyCheckoutReport_Result>> GetResult(Func<proc_EarlyCheckoutReport_Result, bool> condition, long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREemployee, DateTime date)
        {
            object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.Int, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.Int, Value= idHRCompanyDivision??0},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??0},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.Int, Value= idHREemployee??0},
                new SqlParameter() {ParameterName = "@p4", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= ""}
            };
            var result = await UnitOfWork.Db.Database.SqlQuery<proc_EarlyCheckoutReport_Result>("Exec proc_EarlyCheckoutReport @p1,@p2,@paramIdJobStatus,@p3,@p4,@paramSearch", myObjArray).ToListAsync();
            return result.Where(condition).OrderBy(x => x.HRDesignationOrder).ToList();
        }

        public virtual EarlyCheckoutAttendanceReportModel Get(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREemployee, DateTime date)
        {
            object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.Int, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.Int, Value= idHRCompanyDivision??0},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??0},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.Int, Value= idHREemployee??0},
                new SqlParameter() {ParameterName = "@p4", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= ""}
            };
            return UnitOfWork.Db.Database.SqlQuery<EarlyCheckoutAttendanceReportModel>("Exec proc_EarlyCheckoutReport @p1,@p2,@paramIdJobStatus,@p3,@p4,@paramSearch", myObjArray).FirstOrDefault();
        }

        public virtual async Task<bool> SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                object[] obj =
               {
                     new SqlParameter() {ParameterName = "@paramEmail", SqlDbType = SqlDbType.NVarChar, Value = email},
                     new SqlParameter() {ParameterName = "@paramSubject", SqlDbType = SqlDbType.NVarChar, Value= subject},
                     new SqlParameter() {ParameterName = "@paramBody", SqlDbType = SqlDbType.NVarChar, Value= body}
            };
                await UnitOfWork.Db.Database.ExecuteSqlCommandAsync("EXEC SendEmail @paramEmail,@paramSubject,@paramBody", obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<ICollection<proc_EarlyCheckoutReport_Result>> GetPrintResult(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREemployee, DateTime date)
        {
            object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.Int, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.Int, Value= idHRCompanyDivision??0},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??0},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.Int, Value= idHREemployee??0},
                new SqlParameter() {ParameterName = "@p4", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= ""}
            };
            var result = await UnitOfWork.Db.Database.SqlQuery<proc_EarlyCheckoutReport_Result>("Exec proc_EarlyCheckoutReport @p1,@p2,@paramIdJobStatus,@p3,@p4,@paramSearch", myObjArray).ToListAsync();
            return result.OrderBy(x => x.HRDesignationOrder).ToList();
        }
    }
}
