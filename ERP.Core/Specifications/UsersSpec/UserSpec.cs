using ERP.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.UsersSpec
{
    public class UserSpec : BaseSpecifications<Employee>
    {
        public UserSpec(string Id) : base(u => u.Id == Id)
        {
        }
    }
}
