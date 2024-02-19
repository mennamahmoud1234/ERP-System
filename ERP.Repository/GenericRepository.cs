
using ERP.Core.RepositryContract;
using ERP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Core.Data;
using ERP.Core.Specifications;
using System.Linq.Expressions;
namespace ERP.Repository
{
    public class GenericRepository<T> : IGenericRepositry<T> where T : class
    {
        private readonly ERPDBContext _dbContext;

        public GenericRepository(ERPDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddAsync(T entity)
        {    
            await _dbContext.AddAsync(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
           return await ApplySpecifications(spec).ToListAsync();
        }
       

        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id) ?? null;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() // Return all items in Model that not contain any navigation prop
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<int> GetCountAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }


        // this method to apply specification

        private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
        {
            var result =  SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), spec);
            return result;
        }

        public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }


        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            // Assuming your context is named YourDbContext
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

}
