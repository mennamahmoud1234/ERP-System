using ERP.Core.Identity;
using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
    public class Transfer : BaseEntity
    {
        public string Reference { get; set; }
        public int Status { get; set; }
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
        //Navigation Property  [Many] => TransferProduct
        public ICollection<TransferProduct> TransferProducts { get; set; } = new HashSet<TransferProduct>();

        #region Navigation property [One] => Employee
        public string DoneBy { get; set; }
        public Employee Employee { get; set; }
        #endregion
    }
}
