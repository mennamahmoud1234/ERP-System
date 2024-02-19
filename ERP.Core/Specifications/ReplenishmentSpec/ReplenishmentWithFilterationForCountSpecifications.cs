using ERP.Core.Entities;
using ERP.Core.Specifications.Product_Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.ReplenishmentSpec
{
    public class ReplenishmentWithFilterationForCountSpecifications : BaseSpecifications<Replenishment>
    {
        public ReplenishmentWithFilterationForCountSpecifications(ProductSpecParams productSpecParams) 
            : base()
        {
            
        }
    }
}
