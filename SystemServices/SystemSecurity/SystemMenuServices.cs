using System;
using SystemDatabase;
using SystemInterfaces.SystemSecurity;
using SystemModels.SystemSecurity;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.SystemSecurity
{
    public class SystemMenuServices : BaseRepository<SystemMenu, SystemMenuModel>, ISystemMenuServices
    {
        public SystemMenuServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new Exception("unitOfWork");
            }
        }
    }
}
