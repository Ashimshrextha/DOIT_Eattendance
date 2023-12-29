using System;
using SystemDatabase;
using SystemInterfaces.CompanyManagement;
using SystemModels.CompanyManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.CompanyManagement
{
    public class HRCompanyShiftRoasterServices : BaseRepository<HRCompanyShiftRoaster, HRCompanyShiftRoasterModel>, IHRCompanyShiftRoasterServices<HRCompanyShiftRoaster, HRCompanyShiftRoasterModel>
    {
        public HRCompanyShiftRoasterServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }
    }
}
