using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class TaxDto
    {
        public int Id { get; set; }
        [Required]
        public string TaxName { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int? TaxValue { get; set; } 
        [Required]
        public int? TaxType { get; set; }
    }
}
