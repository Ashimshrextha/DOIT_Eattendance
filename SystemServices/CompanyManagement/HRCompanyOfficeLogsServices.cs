using PagedList;
using System;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.CompanyManagement;
using SystemModels.CompanyManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.CompanyManagement
{
    
        public class HRCompanyOfficeLogsServices : BaseRepository<HRCompanyEmployeeOfficeLog, HRCompanyOfficeLogsModel>, IHRCompanyOfficeLogsServices<HRCompanyEmployeeOfficeLog, HRCompanyOfficeLogsModel>
        {

            public HRCompanyOfficeLogsServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitofwork");
            }
        }

        public Task<IPagedList<HRCompanyEmployeeOfficeLog>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            throw new NotImplementedException();
        }

    }
}
