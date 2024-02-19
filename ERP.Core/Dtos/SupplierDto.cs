using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class SupplierDto
    {
        public int Id { get; set; }
        [Required]
        public string SupplierName { get; set; }
        [Required]
        [EmailAddress]
        public string SupplierEmail { get; set; }
        [Required]
        [Phone]
        public string SupplierPhone { get; set; }
        public string? AddedBy { get; set; }

    }
}
