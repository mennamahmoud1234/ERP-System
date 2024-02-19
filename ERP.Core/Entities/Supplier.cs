using ERP.Core.Identity;
using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERP.Core.Entities
{
    public class Supplier : BaseEntity
    {
        public string SupplierName { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierPhone { get; set; }

        #region  Navigation property [one] => Employee
        public string AddedBy { get; set; } 
        public Employee Employee { get; set; }
        #endregion
        
        //Navigation property [Many] => Invoice
        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();

        //Navigation property [Many] => Payment
        public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

    }
}
