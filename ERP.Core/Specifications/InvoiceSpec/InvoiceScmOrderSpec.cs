using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.InvoiceSpec
{
     public class InvoiceScmOrderSpec : BaseSpecifications<Invoice>
    {
        public InvoiceScmOrderSpec(int ScmId) : base((I => I.ScmOrderId == ScmId))
        {
           
        }

    }

}
