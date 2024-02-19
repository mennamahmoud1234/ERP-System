using ERP.Core.Entities;
using ERP.Core.Specifications.Product_Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.OrderSpec
{
    public class InventoryOrderWithAllNavigationsSpecifications : BaseSpecifications<InventoryOrder>
    {
        public InventoryOrderWithAllNavigationsSpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(IO => IO.AccEmp);
            Includes.Add(IO => IO.InventoryEmp);
            Includes.Add(IO => IO.Product);
        }

       
    }
}
