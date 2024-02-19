using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class OrderToUpdateDto
    {
        [Required]
        public string AccEmployeeId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [MaxLength(50)]
        public string Reference { get; set; }
    }
}
