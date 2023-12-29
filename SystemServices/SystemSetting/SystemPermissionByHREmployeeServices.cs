using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.SystemSetting;
using SystemModels.SystemSetting;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.SystemSetting
{
    public class SystemPermissionByHREmployeeServices : BaseRepository<SystemPermissionByHREmployee, SystemPermissionByHREmployeeModel>, ISystemPermissionByHREmployeeServices<SystemPermissionByHREmployee>
    {
        public SystemPermissionByHREmployeeServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<SystemPermissionByHREmployee>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                var model = await FindAllAsync(x => x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == "");
                return model.OrderBy(orderingBy + " " + orderingDirection)
                .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IEnumerable<T>> EmployeeMenuSetupList<T>(long? idHREmployee)
        {
            object[] obj =
            {
                new SqlParameter{ParameterName="@p1",SqlDbType=SqlDbType.Int, Value=idHREmployee}
            };
            return await ExecuteProcedure<T>("exec proc_EmployeeMenuSetupList @p1", obj);
        }
    }
}
