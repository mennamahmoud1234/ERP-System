using ERP.Core.Dtos;
using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Services.Contract
{
    public interface ISCMService
    {
        Task<ApiResponseDto<SupplierDto>> AddSupplierAsync(Supplier supplier);
        Task<ApiResponseDto<SupplierDto>> UpdateSupplierAsync(SupplierDto supplier);

        #region ScmOrder
        // Create ScmOrder
        Task<ApiResponseDto<ScmOrderProduct>> CreateScmOrderAsync(ScmOrderDto scmOrderProduct, string EmpolyeeId);
        Task<IReadOnlyList<ScmOrder>> GetAllScmOrderAsync();
        Task<IReadOnlyList<ScmOrder>> GetAllScmOrderStatusAsync();

        #endregion 

    }
}
