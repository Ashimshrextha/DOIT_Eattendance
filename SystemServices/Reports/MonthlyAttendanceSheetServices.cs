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
    public class MonthlyAttendanceSheetServices : IMonthlyAttendanceSheetServices<proc_GetMonthlyAttendanceTimeSheetReport_Result>
    {
        public IUnitOfWork UnitOfWork { get; }
        public MonthlyAttendanceSheetServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<ICollection<proc_GetMonthlyAttendanceTimeSheetReport_Result>> GetResult(long? IdHRCompany, long? HRCompanyDivision, int? idJobStatus, long? IdHREemployee, DateTime FromDate, DateTime ToDate)
        {
            object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= IdHRCompany},
                new SqlParameter() {ParameterName = "@paramidDivision", SqlDbType = SqlDbType.BigInt, Value= HRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= IdHREemployee},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= FromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = ToDate},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = ""}
            };
            return await UnitOfWork.Db.Database.SqlQuery<proc_GetMonthlyAttendanceTimeSheetReport_Result>("Exec proc_GetMonthlyAttendanceTimeSheetReport @paramIdHRCompany,@paramIdHREmployee,@paramidDivision,@paramIdJobStatus,@paramFromDate,@paramToDate,@paramSearch", myObjArray).ToListAsync();
        }

        public virtual async Task<IPagedList<proc_GetMonthlyAttendanceTimeSheetReport_Result>> GetResult(Func<proc_GetMonthlyAttendanceTimeSheetReport_Result, bool> condition, long? IdHRCompany, long? HRCompanyDivision, int? idJobStatus, long? IdHREemployee, DateTime FromDate, DateTime ToDate, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= IdHRCompany},
                new SqlParameter() {ParameterName = "@paramIdDivision", SqlDbType = SqlDbType.BigInt, Value= HRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= IdHREemployee},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= FromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = ToDate},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey}
            };
            return (await UnitOfWork.Db.Database.SqlQuery<proc_GetMonthlyAttendanceTimeSheetReport_Result>("Exec proc_GetMonthlyAttendanceTimeSheetReport @paramIdHRCompany,@paramIdHREmployee,@paramidDivision,@paramIdJobStatus,@paramFromDate,@paramToDate,@paramSearch", myObjArray).ToListAsync()).Where(condition).OrderBy(orderingBy + " " + orderingDirection).ToPagedList(pageNumber, pageSize);
        }
    }
}
