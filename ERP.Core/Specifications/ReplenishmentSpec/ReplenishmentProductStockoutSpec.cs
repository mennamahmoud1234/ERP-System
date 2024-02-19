using ERP.Core.Entities;
using ERP.Core.Specifications.Product_Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.ReplenishmentSpec
{
      public class ReplenishmentProductStockoutSpec :BaseSpecifications<Replenishment>
    {
        public ReplenishmentProductStockoutSpec(ProductSpecParams productSpecParams) :base(R=>R.ProductMinquantity < 5) 
        {
            Includes.Add(R=>R.Product);
            AddPagination((productSpecParams.PageIndex - 1) * productSpecParams.PageSize, productSpecParams.PageSize);
        }
    }
}
