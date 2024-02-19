using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.RepositryContract
{
    public interface ICategoryRepo
    {
        Task<ParentCategory?> GetByNameAsync(string Name); // return specific entity by name in db
    }
}
