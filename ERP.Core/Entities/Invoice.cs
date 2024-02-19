using ERP.Core.Identity;
using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Core.Entities
{
    public class Invoice :BaseEntity
    {
       
        public DateTimeOffset BillDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset DueDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Total { get; set; }
        public decimal TaxValue { get; set; }
        public decimal Paid { get; set; }
        public decimal ToPay { get; set; }

        public Invoice()
        {
            // Generate GUID 
            InvoiceNumber = Guid.NewGuid().ToString();
            DueDate = BillDate.AddDays(14);
            
            ToPay = Total - Paid;


        }

        #region Navigational property [One] => Payment
        public ICollection<Payment> Payment { get; set; } =  new HashSet<Payment>();

        #endregion

        #region Navigational property [One] => Employee
        public string OrderBy { get; set; }
        public Employee Employee { get; set; } 
        #endregion

        #region Navigational property [One] => Supplier
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        #endregion

        #region Navigational property [One] => InventoryOrder
        public int? InventoryOrderId { get; set; }
        [ForeignKey(nameof(InventoryOrderId))]
        public InventoryOrder? InventoryOrder { get; set; }
        #endregion

        #region Navigational property [One] => ScmOrder
        public int? ScmOrderId { get; set; }
        [ForeignKey(nameof(ScmOrderId))]
        public ScmOrder? ScmOrder { get; set; }
        #endregion

        #region Navigational Property [One] => Tax
        public int TaxID { get; set; }
        public Tax Tax { get; set; }
        #endregion



    }
}
