using ERP.Core.Identity;
using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
    public class Payment : BaseEntity
    {

        public decimal Amount { get; set; }

        public DateTimeOffset PaymentDate { get; set; } = DateTimeOffset.UtcNow;

        #region Navigational Property [One] => Invoice
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        #endregion

        #region Navigational Property [One] => Employee
        public string DoneBy { get; set; }
        public Employee Employee { get; set; }
        #endregion

        #region Navigational Property [one] => Supplier
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        #endregion

    }
}
