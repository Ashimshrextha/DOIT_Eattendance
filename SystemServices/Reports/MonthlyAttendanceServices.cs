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
    public class MonthlyAttendanceServices : IMonthlyAttendanceServices<proc_MonthlyAttendance_Result>
    {
        public IUnitOfWork UnitOfWork { get; }

        public MonthlyAttendanceServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<ICollection<proc_MonthlyAttendance_Result>> GetResult(long? IdHRCompany, long? HRCompanyDivision, int? idJobStatus, long? IdHREemployee, DateTime FromDate, DateTime ToDate)
        {
            object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= IdHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value= HRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= IdHREemployee},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= FromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = ToDate},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = ""}
            };
            return await UnitOfWork.Db.Database.SqlQuery<proc_MonthlyAttendance_Result>("Exec proc_MonthlyAttendance @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramFromDate,@paramToDate,@paramSearch", myObjArray).ToListAsync();
        }

        public virtual async Task<IPagedList<proc_MonthlyAttendance_Result>> GetResult(Func<proc_MonthlyAttendance_Result, bool> condition, long? IdHRCompany, long? HRCompanyDivision, int? idJobStatus, long? IdHREemployee, DateTime FromDate, DateTime ToDate, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] myObjArray =
                 {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= IdHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value= HRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= IdHREemployee},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= FromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = ToDate},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey}
            };
                var model = (await UnitOfWork.Db.Database.SqlQuery<proc_MonthlyAttendance_Result>("EXEC proc_MonthlyAttendance @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramFromDate,@paramToDate,@paramSearch", myObjArray).ToListAsync());
                return model.OrderBy(orderingBy + " " + orderingDirection).ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
