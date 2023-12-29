using PagedList;
using System;
using System.Data.Entity;
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
    public class HREmployeeBiometricTemplateServices : BaseRepository<HREmployeeBiometricTemplate, HREmployeeBiometricTemplateModel>, IHREmployeeBiometricTemplateServices<HREmployeeBiometricTemplate>
    {
        public HREmployeeBiometricTemplateServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<HREmployeeBiometricTemplate>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                var model = await FindAllAsync(x => x.IdHREmployee.ToString().ToUpper().Contains(searchKey.ToString().ToUpper()));
                return model.OrderBy(orderingBy + " " + orderingDirection)
                .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<DeviceConnectionComponentModel> GetConnectionComponentAsync(Guid idHRDevice)
        {
            try
            {
                var deviceModel = await UnitOfWork.Db.Set<HRDevice>().FindAsync(idHRDevice);
                return new DeviceConnectionComponentModel
                {
                    IPAddress = deviceModel.IPAddress,
                    PortNumber = (int)deviceModel.CommunicationPort,
                    CommunicationKey = 0,
                    DeviceNumber = deviceModel.DeviceNumber,
                    IdDeviceModel = deviceModel.IdHRDeviceModel
                };
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
