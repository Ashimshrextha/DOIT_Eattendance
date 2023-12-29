using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SystemUnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();
        DbContext Db { get; }
    }
}
