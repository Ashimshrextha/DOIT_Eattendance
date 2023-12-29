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
    public class DailyReportServices : IDailyReportServices<proc_DailyAttendanceReport_Result>
    {
        public IUnitOfWork UnitOfWork { get; }
        public DailyReportServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("UnitOfWork");
            }
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<ICollection<proc_DailyAttendanceReport_Result>> GetResult(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREemployee, DateTime date)
        {
            object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.BigInt, Value= idHREemployee},
                new SqlParameter() {ParameterName = "@p4", SqlDbType = SqlDbType.Date, Value= date},
                new SqlParameter() {ParameterName = "@paramSearchKey", SqlDbType = SqlDbType.NVarChar, Value= ""},
            };
            return (await UnitOfWork.Db.Database.SqlQuery<proc_DailyAttendanceReport_Result>("Exec proc_DailyAttendanceReport @p1,@p2,@paramIdJobStatus,@p3,@p4,@paramSearchKey", myObjArray).ToListAsync());
        }

        public virtual async Task<IPagedList<proc_DailyAttendanceReport_Result>> GetPagedList(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@p4", SqlDbType = SqlDbType.Date, Value= date},
                new SqlParameter() {ParameterName = "@paramSearchKey", SqlDbType = SqlDbType.NVarChar, Value= searchKey}
            };
                return (await UnitOfWork.Db.Database.SqlQuery<proc_DailyAttendanceReport_Result>("Exec proc_DailyAttendanceReport @p1,@p2,@paramIdJobStatus,@p3,@p4,@paramSearchKey", myObjArray).ToListAsync()).OrderBy("HRDesignationRank ASC")
                     .ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
