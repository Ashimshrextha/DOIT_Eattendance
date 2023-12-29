using PagedList;
using System;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.Feedback;
using SystemModels.Feedback;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.SystemFeedback
{
    public class SystemCommentResponseServicescs : BaseRepository<SystemCommentResponse, SystemCommentResponseModel>, ISystemCommentservices<SystemCommentResponse>
    {
        public SystemCommentResponseServicescs(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public Task<IPagedList<SystemCommentResponse>> GetsByEmployee(long? idEmployee, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<SystemCommentResponse>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            throw new NotImplementedException();
        }
    }
}
