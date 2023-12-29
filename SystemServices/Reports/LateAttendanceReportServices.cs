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
    public class LateAttendanceReportServices : ILateAttendanceReportServices<proc_LateAttendance_Result>
    {
        public IUnitOfWork UnitOfWork { get; }
        public LateAttendanceReportServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("UnitOfWork");
            }
            UnitOfWork = unitOfWork;
        }
        public virtual async Task<ICollection<proc_LateAttendance_Result>> GetResult(Func<proc_LateAttendance_Result, bool> condition, long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREemployee, DateTime date)
        {
            object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.Int, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.Int, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.Int, Value= idHREemployee},
                new SqlParameter() {ParameterName = "@p4", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= ""}
            };
            var result = await UnitOfWork.Db.Database.SqlQuery<proc_LateAttendance_Result>("Exec proc_LateAttendance @p1,@p2,@paramIdJobStatus,@p3,@p4,@paramSearch", myObjArray).ToListAsync();
            return result.Where(condition).OrderBy(x => x.HRDesignationOrder).ToList();
        }
        public virtual async Task<IPagedList<proc_LateAttendance_Result>> GetPagedList(Func<proc_LateAttendance_Result, bool> condition, long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@p4", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= searchKey}
            };
                var model = await UnitOfWork.Db.Database.SqlQuery<proc_LateAttendance_Result>("Exec proc_LateAttendance @p1,@p2,@paramIdJobStatus,@p3,@p4,@paramSearch", myObjArray).ToListAsync();
                return model.Where(condition).OrderBy("HRDesignationOrder ASC")
                    .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
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
    }
}
