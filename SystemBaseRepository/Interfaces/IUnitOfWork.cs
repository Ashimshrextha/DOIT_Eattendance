using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace SystemBaseRepository.Interfaces
{
    interface IUnitOfWork:IDisposable
    {
        Task Commit();
        DbContext Db { get; }
    }
}
