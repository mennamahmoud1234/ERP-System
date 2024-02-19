using AutoMapper;
using ERP.APIs.Errors;
using ERP.APIs.Extensions;
using ERP.APIs.Helper;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Identity;
using ERP.Core.Services.Contract;
using ERP.Core.Specifications.Product_Spec;
using ERP.Core.Specifications.SupplierSpec;
using ERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ERP.APIs.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        private readonly ISCMService _scmService;
        private readonly IAccountingService _accountingService;

        public SupplierController(IMapper mapper, UserManager<Employee> userManager,ISCMService scmService,IAccountingService accountingService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _scmService = scmService;
            _accountingService = accountingService;
        }
        #region Get All Supplier
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetSuppliers([FromQuery] SupplierSpecParams supplierSpecParams)
        {
            var Suppliers = await _accountingService.GetSuppliersAsync(supplierSpecParams);
            var Count = await _accountingService.GetCountOfSuppliersAsync(supplierSpecParams); // Query For Count all Data that Must return without Pagination 
            var mappedSuppliers = _mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDto>>(Suppliers);
            return Ok(new Pagination<SupplierDto>(supplierSpecParams.PageIndex, supplierSpecParams.PageSize, Count, mappedSuppliers));
        }
        #endregion

        #region 3. Get Specific Supplier

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetSupplier(int id)
        {
            var Supplier = await _accountingService.GetSupplierAsync(id);
            if (Supplier == null)
                return Ok(new List<SupplierDto>());
            var mappedProduct = _mapper.Map<Supplier, SupplierDto>(Supplier);

            return Ok(mappedProduct);
        }
        #endregion

        #region Add Suppliers
        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<SupplierDto>>> AddSupplier(SupplierDto supplier)
        {
            var user = await _userManager.FindUserAsync(User);
            supplier.AddedBy = user.Id;
            supplier.AddedBy = user.Id;
            var mappedSupplier = _mapper.Map<SupplierDto, Supplier>(supplier);
            var addedSupplier = await _scmService.AddSupplierAsync(mappedSupplier);
            if (addedSupplier.Status == 400)
                return BadRequest(new { addedSupplier.Status, addedSupplier.Message });
            if (addedSupplier.Status == 500)
                return StatusCode(500, addedSupplier.Message);

            addedSupplier.Data = _mapper.Map<Supplier, SupplierDto>(mappedSupplier);
            return Ok(addedSupplier);

        }
        #endregion

        #region Update Suppliers
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<SupplierDto>>> UpdateSupplier(SupplierDto supplier ,int id )
        {
            supplier.Id = id;
            var addedSupplier = await _scmService.UpdateSupplierAsync(supplier);
            if (addedSupplier.Status == 400)
                return BadRequest(new { addedSupplier.Status, addedSupplier.Message });
            if (addedSupplier.Status == 500)
                return StatusCode(500, new { Status = 500, addedSupplier.Message });

            return Ok(addedSupplier);

        }
        #endregion
    }
}
