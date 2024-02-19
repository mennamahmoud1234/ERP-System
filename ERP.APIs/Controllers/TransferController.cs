using AutoMapper;
using ERP.APIs.Extensions;
using ERP.APIs.Helper;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Services.Contract;
using ERP.Core.Specifications.Product_Spec;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.APIs.Controllers
{
    [UserAuthorize]
    public class TransferController : BaseController
    {
        #region Constructor
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public TransferController(IInventoryService inventoryService, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }
        #endregion


        #region 14. See all the transfers and the adjustments that have been done to the warehouse.

        //[Authorize]
        [HttpGet("AllTransfers")]
        public async Task<ActionResult<Pagination<TransfersReturnDto>>> GetTransfers([FromQuery] ProductSpecParams productSpecParams)
        {
            var Transfers = await _inventoryService.GetTransfersAsync(productSpecParams);
            var Count = await _inventoryService.GetCountOfTransfersAsync(productSpecParams); // Query For Count all Data that Must return without Pagination 

            var mappedTransfers = _mapper.Map<IEnumerable<Transfer>, IEnumerable<TransfersReturnDto>>(Transfers);
            return Ok(new Pagination<TransfersReturnDto>(productSpecParams.PageIndex, productSpecParams.PageSize, Count, mappedTransfers));
        }

        #endregion
    }
}
