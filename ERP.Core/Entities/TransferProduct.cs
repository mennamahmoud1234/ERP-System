using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
     public class TransferProduct: BaseEntity
    { 
        public int Quantity { get; set; }
        #region Navigation property [one] => transfer
        public int TransferId { get; set; }
        public Transfer Transfer { get; set; } 
        #endregion

        #region Navigation property [one] => Product
        public int ProductId { get; set; }
        public Product Product { get; set; } 
        #endregion

    }
}
