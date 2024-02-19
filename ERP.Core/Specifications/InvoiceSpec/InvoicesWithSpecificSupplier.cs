using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.InvoiceSpec
{
    public class InvoicesWithSpecificSupplier : BaseSpecifications<Invoice>
    {
        public InvoicesWithSpecificSupplier(int Id) : base(I => I.SupplierId == Id ) 
        {
            Includes.Add(I => I.Employee);
            Includes.Add(I => I.Payment);
            Includes.Add(I => I.Supplier);
            Includes.Add(I => I.InventoryOrder);
            Includes.Add(I => I.ScmOrder);
            Includes.Add(I => I.Tax);
        }

       
       

    }
}
