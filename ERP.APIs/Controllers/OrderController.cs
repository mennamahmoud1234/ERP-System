using AutoMapper;
using ERP.APIs.Errors;
using ERP.APIs.Extensions;
using ERP.APIs.Helper;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Identity;
using ERP.Core.RepositryContract;
using ERP.Core.Services.Contract;
using ERP.Core.Specifications.Product_Spec;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERP.APIs.Controllers
{
    [UserAuthorize]
    public class OrderController : BaseController
    {
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        public OrderController(IInventoryService inventoryService, IMapper mapper, UserManager<Employee> userManager)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
            _userManager = userManager;
        }

        #region AddOrder
        [HttpPost] // Post :/api/order
       public async Task<ActionResult<ApiResponseDto<OrderDto>>> CreateOrder([FromBody] OrderDto order)
        {
            if (order.Quantity == 0)
                return BadRequest(new { Status = 400, Message = "Quantity field is required more than one" });

            if (order.ProductId == 0)
                return BadRequest(new { Status = 400, Message = "Quantity field is required more than one" });

            var user = await _userManager.FindUserAsync(User);

            var result = await _inventoryService.CreateInventoryOrderAsync(order, user.Id);

            if (result.Status == 200)
            {
              
                return new ApiResponseDto<OrderDto>()
                {
                    Status = 200,
                    Message = "Created Successfully",
                    Data = order
                };
            }
            else
            {
                return BadRequest(new{ result?.Status ,result?.Message});
            }
        }

        #endregion

        #region MyRegion
        [HttpPut("{orderId}")]
        public async Task<ActionResult<ApiResponseDto<OrdertToReturnDto>>> UpdateOrder([FromBody] OrderToUpdateDto order, int OrderId)
        {
            if (order.Quantity == 0)
                return BadRequest(new { Status = 400, Message = "Quantity field is required more than one" });
            var updateOrder = await _inventoryService.UpdateInventoryOrderAsync(order, OrderId);
            if (updateOrder.Status == 400)
                return BadRequest(new { updateOrder.Status, updateOrder.Message });
            if (updateOrder.Status == 500)
                return StatusCode(500, new { Status = 500, updateOrder.Message });
            return Ok(updateOrder);
        }
        #endregion
        
     

        #region 1. Check the number of the orders from the inventory.

        [HttpGet("OrdersNumber")]
        public async Task<IActionResult> GetOrdersNumber()
        {
            var InventoryOrderNumber = await _inventoryService.GetInventoryOrdersNumberAsync();
            var ScmOrderNumber = await _inventoryService.GetScmOrdersNumberAsync();

            return Ok(new
            {
                InventoryOrderCount = InventoryOrderNumber,
                ScmOrderCount = ScmOrderNumber
            });
        }

        #endregion

        #region 2- Check the orders from the inventory:

        [HttpGet("AllInventoryOrders")]
        public async Task<ActionResult<Pagination<InventoryOrderDto>>> GetAllInventoryOrders([FromQuery] ProductSpecParams productSpecParams)
        {
            var inventoryOrders = await _inventoryService.GetAllInventoryOrdersAsync(productSpecParams);
            var Count = await _inventoryService.GetCountOfInventoryOrderAsync();

            var mappedInventoryOrders = _mapper.Map<IEnumerable<InventoryOrder>, IEnumerable<InventoryOrderDto>>(inventoryOrders);
            return Ok(new Pagination<InventoryOrderDto>(productSpecParams.PageIndex, productSpecParams.PageSize, Count, mappedInventoryOrders));
        }


        #endregion

        #region 3- Check the details of specific order:

        [HttpGet("InventoryOrder/{id}")]
        public async Task<ActionResult<ApiResponseDto<InventoryOrderDetailsDto>>> GetInventoryOrder(int id)
        {
            if(id <= 0)
                return BadRequest(new { Status = 400, Message = "Invalid Inventory Order ID" });
            var inventoryOrder = await _inventoryService.GetInventoryOrderAsync(id);
            if (inventoryOrder.Status == 200)
            {
                return new ApiResponseDto<InventoryOrderDetailsDto>
                {
                    Status = inventoryOrder.Status,
                    Message = inventoryOrder.Message,
                    Data = _mapper.Map<InventoryOrder, InventoryOrderDetailsDto>(inventoryOrder.Data)

                };
            }
            return new ApiResponseDto<InventoryOrderDetailsDto>
                {
                    Status = inventoryOrder.Status,
                    Message = inventoryOrder.Message,
            };
        }

        #endregion
    }

}
