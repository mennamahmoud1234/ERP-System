using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.TaxSpec
{
    public class OneTaxWithNameSpecifications : BaseSpecifications<Tax>
    {
        public OneTaxWithNameSpecifications(string Name) : base(t => t.TaxName == Name)
        {
        }
    }
}
