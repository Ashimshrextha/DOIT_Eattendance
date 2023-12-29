using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.CompanyManagement;
using SystemModels.CompanyManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;
using System.Data;
using System.Data.SqlClient;
namespace SystemServices.SystemSetting
{
    public class HREmployeeBulkAttendanceServices : BaseRepository<HREmployeeManualAttendance, HREmployeeManualAttendanceModel>, IHREmployeeManualAttendanceServices<HREmployeeManualAttendance>
    {
        public HREmployeeBulkAttendanceServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }
        public virtual async Task<IPagedList<HREmployeeManualAttendance>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                var model = await FindAllAsync(x => x.FileName.ToUpper().Contains(searchKey.ToString().ToUpper()));
                return model.OrderBy(orderingBy + " " + orderingDirection)
                .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IPagedList<HREmployeeManualAttendance>> GetsByFileName(long? idEmployee, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var model = await FindAllAsync(x => x.IdReuestId == idEmployee && (x.FileName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                return model.OrderBy(orderingBy + " " + orderingDirection)
                .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public async Task<IPagedList<proc_GetEmployeeBulkAttendance_Result>> GetPagedList(long? idHRCompany, long? idHREmployee, int? idRoleType, int? active, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
            };
                return (await ExecuteProcedure<proc_GetEmployeeBulkAttendance_Result>("EXEC proc_GetEmployeeBulkAttendance @paramIdHRCompany,0", obj))
                     .OrderBy(orderingBy + " " + orderingDirection)
                     .ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
