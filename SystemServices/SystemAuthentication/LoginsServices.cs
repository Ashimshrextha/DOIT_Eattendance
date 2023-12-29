using System;
using SystemDatabase;
using SystemInterfaces.SystemAuthentication;
using SystemModels.SystemAuthentication;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.SystemAuthentication
{
    public class LoginsServices : BaseRepository<Login, LoginsModel>, ILoginsServices<Login>
    {
        public LoginsServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new Exception("unitOfWork");
            }
        }
    }
}
