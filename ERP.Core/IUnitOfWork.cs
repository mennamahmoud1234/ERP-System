using ERP.Core.RepositryContract;
using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core
{
     public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepositry<T> Repositry<T>() where T : class;

        Task<int> CompleteAsync();

         
    }
}
