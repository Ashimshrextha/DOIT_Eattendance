using PagedList;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.SystemSetting;
using SystemModels.EmployeeManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.SystemSetting
{
    public class HRDesignationServices : BaseRepository<HRDesignation, HRDesignationModel>, IHRDesignationServices<HRDesignation>
    {
        public HRDesignationServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<HRDesignation>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                var model = await FindAllAsync(x => x.HRDesignationTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == "");
                return model.OrderBy(orderingBy + " " + orderingDirection)
                .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public virtual async Task<IPagedList<HRDesignation>> GetsByCompany(long? idCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var model = await FindAllAsync(x => x.IdHRCompany == idCompany && (x.HRDesignationShortName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
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
