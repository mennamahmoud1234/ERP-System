using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class SubCategoryToReturnDto
    {
        public int SubCategoryId { get; set; }
        [MaxLength(50)]
        public string SubCategoryName { get; set; }
    }
}
