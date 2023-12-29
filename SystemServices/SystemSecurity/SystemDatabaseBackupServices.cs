using PagedList;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.SystemSecurity;
using SystemModels.SystemSetting;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.SystemSecurity
{
    public class SystemDatabaseBackupServices : BaseRepository<SystemDatabaseBackup, SystemDatabaseBackupModel>, ISystemDatabaseBackupServices<SystemDatabaseBackup>
    {
        public SystemDatabaseBackupServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<SystemDatabaseBackup>> GetBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                var model = await FindAllAsync(x => x.Title.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == "");
                return model.OrderBy(orderingBy + " " + orderingDirection)
                 .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<bool> CreateBackupDatabase()
        {
            try
            {
                await ExecuteProcedure("EXEC proc_Backupdatabase");
                return true;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
