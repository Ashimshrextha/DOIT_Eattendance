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
    public class SystemPermissionByRoleServices : BaseRepository<SystemPermissionByRole, SystemPermissionByRoleModel>, ISystemPermissionByRoleServices<SystemPermissionByRole>
    {
        public SystemPermissionByRoleServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<SystemPermissionByRole>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                var model = await FindAllAsync(x => x.HREmployeeRole.HRRoleTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == "");
                return model.OrderBy(orderingBy + " " + orderingDirection)
                .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IEnumerable<T>> RoleMenuSetupList<T>(int? idRole, long? idHREmployee)
        {
            object[] obj =
            {
                new SqlParameter{ParameterName="@p1",SqlDbType=SqlDbType.Int, Value=idRole},
                new SqlParameter{ParameterName="@p2",SqlDbType=SqlDbType.BigInt, Value=idHREmployee}
            };
            return await ExecuteProcedure<T>("exec proc_RoleMenuSetupList @p1,@p2", obj);
        }
    }
}
