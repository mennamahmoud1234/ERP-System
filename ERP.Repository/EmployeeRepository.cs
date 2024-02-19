using ERP.Core.Data;
using ERP.Core.Identity;
using ERP.Core.RepositryContract;
using ERP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ERPDBContext _dbContext;
        public EmployeeRepository(ERPDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> GetEmployeeAsync(string id)
        {
            return await _dbContext.Set<Employee>().FindAsync(id);
        }
    }
}
