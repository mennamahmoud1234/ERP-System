using ERP.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.RepositryContract
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeAsync(string id);
    }
}
