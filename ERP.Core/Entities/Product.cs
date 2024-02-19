using ERP.Core.Identity;
using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
    public class Product : BaseEntity
    {
        #region Att
        public string ProductName { get; set; }
        public string ProductBarcode { get; set; }
        public int ProductOnHand { get; set; }
        public int ProductInComing { get; set; }
        public int ProductOutGoing { get; set; }
        public decimal ProductSellPrice { get; set; }
        public decimal ProductCostPrice { get; set; }
        public int ActiveOrder { get; set; }
        #endregion
        //Fk For Employee
        public string AddedBy { get; set; }
        //Navigation property [One} => Employee
        public Employee Employee { get; set; }


        //Fk For category
        public int SubCategoryId { get; set; }
        //Navigation property [One} => Category
        public SubCategory SubCategory { get; set; }
       
        //Navigation property [One} => Replenishment
        public virtual Replenishment Replenishment { get; set; }
        //Navigation property many => InventoryOrder
        public ICollection<InventoryOrder> inventoryOrder {  get; set; } = new HashSet<InventoryOrder>();

        //Navigation property many => TransferProduct
        public ICollection<TransferProduct> TransferProducts { get; set; } = new HashSet<TransferProduct>();

        //Navigation property many => MovesHistory
        public ICollection<MovesHistory> MovesHistories { get; set; } = new HashSet<MovesHistory>();

        //Navigation property many => ScmOrderProduct
        public ICollection<ScmOrderProduct> ScmProductOrders { get; set;} = new HashSet<ScmOrderProduct>();

    }

}
