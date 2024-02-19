using ERP.Core.Dtos;
using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Services.Contract
{
     public  interface IHRServices
    {
        Task<IReadOnlyList<Department>> GetDepartments();
        Task<Department> GetSpesificDepartment(int id);
       Task<ApiResponseDto<Department>> CreateDepartment(Department department);
    }
}
