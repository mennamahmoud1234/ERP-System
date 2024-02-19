using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class TransfersReturnDto
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public int Status { get; set; }
        public DateTimeOffset Date { get; set; } 
        public ICollection<TransferProductDto> TransferProducts { get; set; } 
    }
}
