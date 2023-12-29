using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.Reports;
using SystemUnitOfWork.Interfaces;


namespace SystemServices.Reports
{
    public class MonthlyAttendanceSummaryServices : IMonthlyAttendanceSummaryServices<proc_GetMonthlyWorkingSummaryReport_Result>
    {
        public IUnitOfWork UnitOfWork { get; }
        public MonthlyAttendanceSummaryServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("UnitOfWork");
            }
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<ICollection<proc_GetMonthlyWorkingDetailReport_Result>> Gets(long? idHRCompany, long? idHREmployee, DateTime fromDate, DateTime toDate, int yearNP)
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value= toDate},
                 new SqlParameter() {ParameterName = "@paramyearNP", SqlDbType = SqlDbType.Int, Value=yearNP}
            };
                return (await UnitOfWork.Db.Database.SqlQuery<proc_GetMonthlyWorkingDetailReport_Result>("Exec proc_GetMonthlyWorkingDetailReport @paramIdHRCompany,@paramIdHREmployee,@paramyearNP,@paramFromDate,@paramToDate", myObjArray).ToListAsync());
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<proc_GetMonthlyWorkingSummaryReport_Result>> Gets(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime fromDate, DateTime toDate)
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value= toDate},
                 new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= ""}
            };
                return (await UnitOfWork.Db.Database.SqlQuery<proc_GetMonthlyWorkingSummaryReport_Result>("Exec proc_GetMonthlyWorkingSummaryReport @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramFromDate,@paramToDate,@paramSearch", myObjArray).ToListAsync());
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IPagedList<proc_GetMonthlyWorkingSummaryReport_Result>> GetPagedList(Func<proc_GetMonthlyWorkingSummaryReport_Result, bool> condition, long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime fromDate, DateTime toDate, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value= toDate},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= searchKey}
            };
                var model = (await UnitOfWork.Db.Database.SqlQuery<proc_GetMonthlyWorkingSummaryReport_Result>("Exec proc_GetMonthlyWorkingSummaryReport @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramFromDate,@paramToDate,@paramSearch", myObjArray).ToListAsync());
                return model.OrderBy("HRDesignationRank ASC")
                    .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<HREmployeeAttendanceHistory>> GetsByIdAsync(long? idHREmployee, DateTime fromDate, DateTime toDate)
        {
            try
            {
                return await this.UnitOfWork.Db.Set<HREmployeeAttendanceHistory>().Where(x => x.IdHREmployee == idHREmployee && x.AttendanceDate >= fromDate && x.AttendanceDate <= toDate).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IPagedList<proc_GetDateRangeWorkingSummaryReport_Result>> GetRangeUpdate(Func<proc_GetDateRangeWorkingSummaryReport_Result, bool> condition, long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime fromDate, DateTime toDate, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value= toDate},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= searchKey}
            };
                var model = (await UnitOfWork.Db.Database.SqlQuery<proc_GetDateRangeWorkingSummaryReport_Result>("Exec proc_GetDateRangeWorkingSummaryReport @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramFromDate,@paramToDate,@paramSearch", myObjArray).ToListAsync());
                return model.OrderBy("HRDesignationRank ASC")
                    .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }






        public virtual async Task<ICollection<proc_GetDateRangeWorkingDetailReport_Result>> GetsRange(long? idHRCompany, long? idHREmployee,int YearNp,int YearToNP, DateTime fromDate, DateTime toDate)
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value= toDate},
                 new SqlParameter() {ParameterName = "@paramyearNP", SqlDbType = SqlDbType.Int, Value=YearNp},
                 new SqlParameter() {ParameterName = "@paramyearToNP", SqlDbType = SqlDbType.Int, Value=YearToNP}
            };
                return (await UnitOfWork.Db.Database.SqlQuery<proc_GetDateRangeWorkingDetailReport_Result>("Exec proc_GetDateRangeWorkingDetailReport @paramIdHRCompany,@paramIdHREmployee,@paramyearNP,@paramyearToNP,@paramFromDate,@paramToDate", myObjArray).ToListAsync());
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }


        public virtual async Task<ICollection<proc_GetDateRangeWorkingSummaryReport_Result>> GetRange(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime fromDate, DateTime toDate)
        {
            try
            {
                object[] myObjArray =
            {
               new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value= toDate},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= ""}
            };
                return (await UnitOfWork.Db.Database.SqlQuery<proc_GetDateRangeWorkingSummaryReport_Result>("Exec proc_GetDateRangeWorkingSummaryReport @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramFromDate,@paramToDate,@paramSearch", myObjArray).ToListAsync());
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

    }
}
