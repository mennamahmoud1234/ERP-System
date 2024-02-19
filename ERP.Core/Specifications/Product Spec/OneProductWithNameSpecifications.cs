using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.Product_Spec
{
    public class OneProductWithNameSpecifications : BaseSpecifications<Product>
    {
        public OneProductWithNameSpecifications(string Name) : base(p => p.ProductName == Name)
        {
        }
    }
}
