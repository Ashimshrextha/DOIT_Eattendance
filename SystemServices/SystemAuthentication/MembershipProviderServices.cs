using System;
using SystemDatabase;
using SystemInterfaces.SystemAuthentication;
using SystemModels.SystemAuthentication;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.SystemAuthentication
{
    public class MembershipProviderServices : BaseRepository<MembershipProvider, MembershipProviderModel>, IMembershipProviderServices<MembershipProvider>
    {
        public MembershipProviderServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }
    }
}
