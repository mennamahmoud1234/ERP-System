using AutoMapper;
using ERP.APIs.Extensions;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Identity;
using ERP.Core.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ERP.APIs.Controllers
{
    [UserAuthorize]
    public class ScmOrderController : BaseController
    {
        

        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        private readonly ISCMService _scmService;

        public ScmOrderController(IMapper mapper, UserManager<Employee> userManager, ISCMService scmService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _scmService = scmService;
        }
        #region AddScmOrder
        [HttpPost] // Post :/api/ScmOrder
        public async Task<ActionResult<ApiResponseDto<ScmOrderDto>>> CreateScmOrder([FromBody] ScmOrderDto scmOrder)
        {

            var user = await _userManager.FindUserAsync(User);
            var order = await _scmService.CreateScmOrderAsync(scmOrder, user.Id);
            if (order.Status == 200)
            {


                return new ApiResponseDto<ScmOrderDto>()
                {
                    Status = 200,
                    Message = "Created Successfully",
                    Data = scmOrder
                };
            }
            else
            {
                return BadRequest(new { order.Status, order.Message });
            }



        }

        #endregion

        #region GETAllScmOrder
        [HttpGet]// Get :/api/ScmOrder

        public async Task<ActionResult<IReadOnlyList<ScmOrderToReturnDto>>> GetScmOrders()
        {
            var ScmOrder = await _scmService.GetAllScmOrderAsync();
            if (ScmOrder is null)
            {
                return NotFound("There Is No ScmOrder");
            }

            else
            {
                return Ok(_mapper.Map<IReadOnlyList<ScmOrder>, IReadOnlyList<ScmOrderToReturnDto>>(ScmOrder));
            }

        }


        #endregion

        #region GETAllScmOrder
        [HttpGet("ScmOrderStatus")]// Get :/api/ScmOrder/ScmOrderStatus

        public async Task<ActionResult<IReadOnlyList<OrderStatusDto>>> GetScmOrdersStatus()
        {
            var OrderStatus= await _scmService.GetAllScmOrderStatusAsync();
            if(OrderStatus is null)
            { return NotFound("There Is No OrderStatus Exist"); }

            else
            {
                return Ok(_mapper.Map<IReadOnlyList<ScmOrder>,IReadOnlyList<OrderStatusDto>>(OrderStatus));
            }



        }
       
        
            

        


    #endregion


 }
 
}
