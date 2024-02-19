using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string SubCategoryName { get; set; }
        [Required]
        public int ParentCategoryId { get; set; }
    }
}
