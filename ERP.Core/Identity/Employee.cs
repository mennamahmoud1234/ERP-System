using ERP.Core.Entities;
using ERP.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Identity
{
    public class Employee : IdentityUser
    {
        public bool IsDeleted { get; set; } = false;
        public string EmployeeJob { get; set; }

        #region Navigation property [self] => AddedBy
        public string? AddedById { get; set; }
        public Employee? AddedBy { get; set; } 
        #endregion

        //Navigation property [many] => Product
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

        //Navigation property [many] => MovesHistory
        public ICollection<MovesHistory> MovesHistorris { get; set; } = new HashSet<MovesHistory>();

        //Navigation property [many] => Transfer
        public ICollection<Transfer> Transfers { get; set; } = new HashSet<Transfer>();

        //Navigation property [many] => Transfer
        public ICollection<InventoryOrder> InventoryEmp { get; set; } = new HashSet<InventoryOrder>();

        //Navigation property [many] => Transfer
        //public ICollection<InventoryOrder> ScmEmp { get; set; } = new HashSet<InventoryOrder>();

        //Navigation property [one] => EmployeeWorkInformation

        public EmployeeWorkInformation EmployeeWorkInformations { get; set; }
        //Navigation property [one] => EmployeePrivateInformation
         public EmployeePrivateInformation EmployeePrivateInformations { get; set; }
        //Navigation property [many] => Suppliers
        public ICollection<Supplier> Suppliers { get; set; }=new HashSet<Supplier>();

        //Navigation property [many] => ScmOrder
        public ICollection<ScmOrder> ScmOrders{ get; set; } = new HashSet<ScmOrder>();

        //Navigation property [many] => ScmOrder
        public ICollection<ScmOrder> ScmOrdersAcc { get; set; } = new HashSet<ScmOrder>();
        //Navigation property [many] => InventoryOrder
        public ICollection<InventoryOrder> InventoryOrder { get; set; } = new HashSet<InventoryOrder>();


        #region Navigation property [one] => Department

        public int EmployeeDepartment { get; set; }

        public Department Department { get; set; }
        #endregion

        #region Navigation property [one] => Attend

        //public int?  AttendId{ get; set; }
        //public Attend Attends { get; set; }
        #endregion

        //Navigational Property [Many] => Invoice
        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();

        //Navigational Property [Many] => Payment
        public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

    }
}
