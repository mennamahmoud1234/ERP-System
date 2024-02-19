using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{   
     public class Replenishment : BaseEntity
    {
        public int ProductMinquantity { get; set; }
        public int ProductMaxquantity { get; set;}
        #region Navigation property [One]
        //Fk => Product
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; } 
        #endregion

    }
}
