using ERP.Core.Entities;
using ERP.Core.Specifications.Product_Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.OrderSpec
{
    public class InventoryOrderSpecification : BaseSpecifications<InventoryOrder>
    {
        public InventoryOrderSpecification(ProductSpecParams productSpecParams):base()
        {
            AddPagination((productSpecParams.PageIndex - 1) * productSpecParams.PageSize, productSpecParams.PageSize);

        }

        public InventoryOrderSpecification(ProductSpecParams productSpecParams ,string AccEmployee) : base((I=>I.AccEmployee ==AccEmployee && I.Status==0))
        {
            AddPagination((productSpecParams.PageIndex - 1) * productSpecParams.PageSize, productSpecParams.PageSize);
            Includes.Add(IO => IO.AccEmp);
            Includes.Add(IO => IO.InventoryEmp);
            Includes.Add(IO => IO.Product);

        }

    }
}
