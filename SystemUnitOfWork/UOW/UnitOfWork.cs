using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using SystemDatabase;
using SystemUnitOfWork.Interfaces;

namespace SystemUnitOfWork.UOW
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private EAttendanceSystemDBEntities _dbContext;
        private bool disposed = false;

        public UnitOfWork()
        {
            _dbContext = new EAttendanceSystemDBEntities();
        }


        public DbContext Db
        {
            get { return _dbContext; }
        }

        public async Task SaveAsync()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {                   
                        await _dbContext.SaveChangesAsync();                 
                                              
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (_dbContext != null)
                    {
                        _dbContext.Dispose();
                        _dbContext = null;
                    }
                }
            }
            this.disposed = true;
        }
    }
}
