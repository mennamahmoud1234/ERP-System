using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Core.Entities;
using ERP.Core.Specifications;


namespace ERP.Core.RepositryContract
{
    public interface IGenericRepositry<T> where T : class
    {
        Task<T?> GetAsync(int id); // return specific entity in db

        Task <IReadOnlyList<T>> GetAllAsync(); // return All entity in db
       
        Task AddAsync(T entity); // Add Entity to db 
        void Update(T entity);   // Update Entity in db

      

        Task<int> GetCountAsync();// Return Count all items 
        Task<int> GetCountAsync(ISpecifications<T> spec);// Return Count all items 

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);// Return all items in Model that contain navigation prop
        Task<T?> GetWithSpecAsync(ISpecifications<T> spec);// Return one item in Model that contain navigation prop


        Task DeleteAsync(T entity);

    }

}
