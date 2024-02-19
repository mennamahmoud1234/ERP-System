using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.InvoiceSpec
{
   public class InvoiceInventorySpec :BaseSpecifications<Invoice>
    {
        public InvoiceInventorySpec (int inventoryId) : base((I => I.InventoryOrderId==inventoryId))
        {

        }
        public InvoiceInventorySpec(string userId) : base((I => I.OrderBy == userId))
        {
            Includes.Add(i => i.Supplier);
            Includes.Add(i => i.Tax);
            Includes.Add(i => i.Payment);
        }

    }
}
