using ERP.Core.Data;
using ERP.Core.Entities;
using ERP.Core.Identity;
using ERP.Core.RepositryContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Repository
{
    public class CategoryRepo : GenericRepository<ParentCategory>, ICategoryRepo
    {
        private readonly ERPDBContext _dbContext;
        public CategoryRepo(ERPDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ParentCategory?> GetByNameAsync(string Name)
        {
            return await _dbContext.Set<ParentCategory>().FirstOrDefaultAsync(entity => entity.ParentCategoryName == Name)??null;
        }

    }
}
