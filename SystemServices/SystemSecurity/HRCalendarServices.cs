using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.SystemSecurity;
using SystemModels.SystemSecurity;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.SystemSecurity
{
    public class HRCalendarServices : BaseRepository<HRCalendar, HRCalendarModel>, IHRCalendarServices<HRCalendar>
    {
        public HRCalendarServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<HRCalendar>> GetBySearchKey(int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                await Task.WhenAll();
                return _dbSet.Where(x => x.FestivalName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.NepDate.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == "").OrderBy(orderingBy + " " + orderingDirection)
                  .ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<proc_GetMonthlyAttendanceHeader_Result>> GetMonthlyAttendanceHeader(int year, int month)
        {
            try
            {
                object[] obj =
               {
                new SqlParameter() {ParameterName = "@paramYear", SqlDbType = SqlDbType.Int, Value= year},
                new SqlParameter() {ParameterName = "@paramMonth", SqlDbType = SqlDbType.Int, Value = month},
            };
                return await UnitOfWork.Db.Database.SqlQuery<proc_GetMonthlyAttendanceHeader_Result>("EXEC proc_GetMonthlyAttendanceHeader @paramYear,@paramMonth", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<proc_MonthlyCalendar_Result>> GetCalendar(long? idHREmployee, DateTime fromDate, DateTime toDate)
        {
            try
            {
                object[] obj =
               {
                    new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = toDate},
            };
                return await UnitOfWork.Db.Database.SqlQuery<proc_MonthlyCalendar_Result>("EXEC proc_MonthlyCalendar @paramIdHREmployee,@paramFromDate,@paramToDate", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
