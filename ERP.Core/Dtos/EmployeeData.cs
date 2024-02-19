using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
     public class EmployeeData
    {
        public string Id { get; set; }

        public string? UserName { get; set; }

        public string EmployeeJob { get; set; }

    }
}
