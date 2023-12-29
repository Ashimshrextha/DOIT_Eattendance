using System;
using SystemDatabase;
using SystemInterfaces.EmployeeManagement;
using SystemModels.EmployeeManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.EmployeeManagement
{
    public class HREmployeeNationalIdentityServices : BaseRepository<HREmployeeNationalIdentity, HREmployeeNationalIdentityModel>, IHREmployeeNationalIdentityServices<HREmployeeNationalIdentity>
    {
        public HREmployeeNationalIdentityServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentException("unitOfWork");
            }
        }
    }
}
