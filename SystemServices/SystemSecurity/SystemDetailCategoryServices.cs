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
    public class SystemDetailCategoryServices : BaseRepository<SystemDetailCategory, SystemDetailCategoryModel>, ISystemDetailCategoryServices
    {
        public SystemDetailCategoryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }
        public virtual async Task<IPagedList<SystemDetailCategory>> GetBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                var model = await FindAllAsync(x => x.CategoryTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == "");
                return model.OrderBy(orderingBy + " " + orderingDirection)
                 .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
