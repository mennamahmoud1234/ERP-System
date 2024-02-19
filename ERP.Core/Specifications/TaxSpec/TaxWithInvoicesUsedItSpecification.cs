using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.TaxSpec
{
    public class TaxWithInvoicesUsedItSpecification : BaseSpecifications<Tax>
    {
        public TaxWithInvoicesUsedItSpecification() : base() 
        {
            Includes.Add(T => T.Invoice);
        }
    }
}
