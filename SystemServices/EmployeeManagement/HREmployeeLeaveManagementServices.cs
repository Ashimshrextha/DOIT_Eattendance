using System;
using SystemDatabase;
using SystemInterfaces.EmployeeManagement;
using SystemModels.EmployeeManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.EmployeeManagement
{
    public class HREmployeeLeaveManagementServices : BaseRepository<HREmployeeLeaveManagement, HREmployeeLeaveManagementModel>, IHREmployeeLeaveManagementServices<HREmployeeLeaveManagement, HREmployeeLeaveManagementModel>
    {
        public HREmployeeLeaveManagementServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitofwork");
            }
        }
       
    }
}
