using AutoMapper;
using ERP.APIs.Errors;
using ERP.APIs.Extensions;
using ERP.APIs.Helper;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Identity;
using ERP.Core.Services.Contract;
using ERP.Core.Specifications.Product_Spec;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ERP.APIs.Controllers
{
    [UserAuthorize]
    public class ProductController : BaseController
    {
        #region Constructor
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public ProductController(IInventoryService inventoryService, IMapper mapper, UserManager<Employee> userManager)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
            _userManager = userManager;
        }
      #endregion

        #region 1. Check the number of the products in the warehouse and check if there’s a Replenishment

        
        [HttpGet("ProductsAndReplenishmentNumbers")]
        public async Task<IActionResult> GetNumberOfProductsAndReplenishment()
        {
            var ProuctsNum = await _inventoryService.GetProductsNumberAsync();
            var ReplenishmentNum = await _inventoryService.GetReplenishmentNumberAsync();
            return Ok(new
            {
                ProductsCount = ProuctsNum,
                ReplenishmentCount = ReplenishmentNum
            });
        }

        #endregion


        #region 2. Check the products in the warehouse:

        //[Authorize]
        [HttpGet("AllProducts")]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productSpecParams)
        {
            var Products = await _inventoryService.GetProductsAsync(productSpecParams);
            var Count = await _inventoryService.GetCountOfProductAsync(productSpecParams); // Query For Count all Data that Must return without Pagination 


            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            return Ok(new Pagination<ProductDto>(productSpecParams.PageIndex, productSpecParams.PageSize, Count, mappedProducts));
        }

        #endregion

        #region 3. See the details of the products in the warehouse

        [ProducesResponseType(typeof(ProductDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsDto>> GetProduct(int id)
        {
            var Product = await _inventoryService.GetProductAsync(id);
            if (Product == null)
                return Ok(new List<ProductDetailsDto>());
            var mappedProduct = _mapper.Map<Product, ProductDetailsDto>(Product);

            return Ok(mappedProduct);
        }
        #endregion




        #region Get ALL Product will Stock Out Sooon => Menna Afifi
        
        [HttpGet("StockOutProduct")]  //Post :/api/Product/StockOutProduct
        public async Task<ActionResult<Pagination<ReplenishmentProductStockOutDto>>> GetAllStockOutProduct([FromQuery] ProductSpecParams productSpecParams)
        {
            var product = await _inventoryService.GetAllProductStockOutsAsync(productSpecParams);
            if (product.Count() == 0)
            {
                return BadRequest(new ApiResponse(404));
            }
            var Count = await _inventoryService.GetCountStockoutAsync(productSpecParams);
            var mappedProduct = _mapper.Map<IEnumerable<Replenishment>, IEnumerable<ReplenishmentProductStockOutDto>>(product);

            return Ok(new Pagination<ReplenishmentProductStockOutDto>(productSpecParams.PageIndex, productSpecParams.PageSize, Count, mappedProduct));

        }
        #endregion


        #region Update Product Data
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<ProductReturnDto>>> UpdateProduct(int Id, [FromBody] ProductReturnDto product)
        {
            var mappedProduct = _mapper.Map<ProductReturnDto, Product>(product);
            mappedProduct.Id = Id;
            var updateProduct = await _inventoryService.UpdateProductAsync(mappedProduct);
            if (updateProduct.Status != 200)
                return BadRequest(new { updateProduct.Status, updateProduct.Message });
            //map product into ProductRetunDto
            updateProduct.Data = _mapper.Map<Product, ProductReturnDto>(mappedProduct);
            return Ok(updateProduct);
        } 
        #endregion

        #region 5. Add product to the warehouse.
        [HttpPost("CreateProduct")]    //Post :/api/Product/CreateProduct
        public async Task<ActionResult<ApiResponseDto<Product>>> CreateProduct(ProductReturnDto product)
        {
            var user = await _userManager.FindUserAsync(User);
            var mappedProduct = _mapper.Map<ProductReturnDto, Product>(product);
            var createdProduct = await _inventoryService.CreateProductAsync(mappedProduct , user.Id);
            if (createdProduct.Status != 200)
                return BadRequest(new { createdProduct.Status, createdProduct.Message });
            //map product into ProductRetunDto
            createdProduct.Data = _mapper.Map<Product, ProductReturnDto>(mappedProduct);
            return Ok(createdProduct);
        }

        #endregion
    }
}
