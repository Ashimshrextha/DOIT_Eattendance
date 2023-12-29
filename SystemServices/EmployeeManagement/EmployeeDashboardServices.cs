using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.EmployeeManagement;
using SystemUnitOfWork.Interfaces;

namespace SystemServices.EmployeeManagement
{
    public class EmployeeDashboardServices : IEmployeeDashboardServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeDashboardServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public virtual async Task<ICollection<proc_GetTotalEmployeeCountByGender_Result>> GetEmployeeGenderCount(long? idHRCompany, int? idRoleType)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_GetTotalEmployeeCountByGender_Result>("EXEC proc_GetTotalEmployeeCountByGender @paramIdHRCompany,@paramIdRoleType", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new ArgumentNullException(exp.Message);
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
                return await _unitOfWork.Db.Database.SqlQuery<proc_MonthlyCalendar_Result>("EXEC proc_MonthlyCalendar @paramIdHREmployee,@paramFromDate,@paramToDate", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
