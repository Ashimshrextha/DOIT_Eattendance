using PagedList;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.DeviceManagement;
using SystemModels.DeviceManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.DeviceManagement
{
    public class HRDeviceModelServices : BaseRepository<HRDeviceModel, HRDeviceModels>, IHRDeviceModelServices<HRDeviceModel>
    {
        public HRDeviceModelServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<HRDeviceModel>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                var model = await FindAllAsync(x => x.HRDeviceModel1.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == "");
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
