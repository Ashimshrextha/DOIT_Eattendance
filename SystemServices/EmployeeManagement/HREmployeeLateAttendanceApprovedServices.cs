using PagedList;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.EmployeeManagement;
using SystemModels.EmployeeManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.EmployeeManagement
{
    public class HREmployeeLateAttendanceApprovedServices : BaseRepository<HREmployeeAttendanceHistory, HREmployeeAttendanceHistoryModel>, IHREmployeeLateAttendanceApprovedServices<HREmployeeAttendanceHistory>
    {
        public HREmployeeLateAttendanceApprovedServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<HREmployeeAttendanceHistory>> GetsByEmployee(long? idEmployee, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                return (await this._dbSet.Where(x => x.IdHREmployee == idEmployee && x.LateBy != null).ToListAsync()).OrderBy(orderingBy + " " + orderingDirection)
                .ToPagedList(pageNumber, pageSize);
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
