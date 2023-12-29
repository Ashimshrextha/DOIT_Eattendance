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
    public class SystemCommentServices : BaseRepository<SystemComment, SystemCommentModel>, ISystemCommentservices<SystemComment>
    {
        public SystemCommentServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public Task<IPagedList<SystemComment>> GetsByEmployee(long? idEmployee, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<SystemComment>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            throw new NotImplementedException();
        }
    }
}
