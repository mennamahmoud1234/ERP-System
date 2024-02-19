using ERP.Core;
using ERP.Core.Data;
using ERP.Core.Entities;
using ERP.Core.RepositryContract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERP.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ERPDBContext _dbContext;

        private Hashtable _repositories;
        
        
        public UnitOfWork(ERPDBContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public Task<int> CompleteAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
           return _dbContext.DisposeAsync();
        }

        // this method to create repositry on demond
        // to avoid declaration all repo
        public IGenericRepositry<T> Repositry<T>() where T : class
        {
            var key = typeof(T).Name;

            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<T>(_dbContext);
                _repositories.Add(key, repository);
            }

            return _repositories[key] as IGenericRepositry<T>;
        }
    }
}
 