using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
    public class Tax : BaseEntity
    {
        public string TaxName { get; set; }
        public int TaxValue { get; set; } //int??
        public int TaxType { get; set; }

        //Navigational Property [Many] => Invoice
        public ICollection<Invoice> Invoice { get; set; } = new HashSet<Invoice>();
    }
}
