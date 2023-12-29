using System;
using SystemDatabase;
using SystemInterfaces.CompanyManagement;
using SystemModels.CompanyManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.CompanyManagement
{
    public class HRCompanyHREmployeeShiftDateServices : BaseRepository<HRCompanyHREmployeeShiftDate, HRCompanyHREmployeeShiftDateModel>, IHRCompanyHREmployeeShiftDateServices<HRCompanyHREmployeeShiftDate, HRCompanyHREmployeeShiftDateModel>
    {
        public HRCompanyHREmployeeShiftDateServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitofwork");
            }
        }
    }
}
