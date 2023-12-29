using PagedList;
using System;
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
    public class MasterLeaveTitleServices : BaseRepository<MasterLeaveTitle, MasterLeaveTitleModel>, IMasterLeaveTitleServices<MasterLeaveTitle>
    {
        public MasterLeaveTitleServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<MasterLeaveTitle>> GetBySearchKey(int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                await Task.WhenAll();
                return _dbSet.Where(x => x.LeaveTitle.ToUpper().Contains(searchKey.ToUpper().ToString()) || x.LeaveTitleNP.ToUpper().Contains(searchKey.ToUpper().ToString()) || searchKey == "").OrderBy(orderingBy + " " + orderingDirection)
                 .ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

    }
}
