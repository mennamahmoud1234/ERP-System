using AutoMapper;
using ERP.APIs.Extensions;
using ERP.APIs.Helper;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Identity;
using ERP.Core.Services.Contract;
using ERP.Core.Specifications.PayementSpec;
using ERP.Core.Specifications.Product_Spec;
using ERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ERP.APIs.Controllers
{
    [UserAuthorize]
    public class AccountingController : BaseController
    {
        private readonly IAccountingService _accountingService;
        private readonly IInventoryService _inventoryService;
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;

        public AccountingController(IAccountingService accountingService, UserManager<Employee> userManager ,IMapper mapper,IInventoryService inventoryService )
        {
            _accountingService = accountingService;
            _userManager = userManager;
            _mapper = mapper;
            _inventoryService = inventoryService;
        }
      
     

        #region GetInventoryOrderRequest
        [HttpGet]
        public async Task<ActionResult<Pagination<InventoryOrderToReturnDto>>> GetInventoryOrderRequest([FromQuery] ProductSpecParams productSpecParams)
        {    
            // Get AccEmployee
            var user = await _userManager.FindUserAsync(User);
            //GetAllOrder
            var inventoryOrders = await _accountingService.GetAllInventoryOrderRequstsAsync(productSpecParams, user.Id);
             //GetCount
            var Count = await _inventoryService.GetCountOfInventoryOrderAsync();

            var mappedInventoryOrders = _mapper.Map<IEnumerable<InventoryOrder>, IEnumerable<InventoryOrderToReturnDto>>(inventoryOrders);
            return Ok(new Pagination<InventoryOrderToReturnDto>(productSpecParams.PageIndex, productSpecParams.PageSize, Count, mappedInventoryOrders));
        }


        #endregion

         #region GetScmOrderRequest
        [HttpGet("ScmOrder")]
        public async Task<ActionResult<Pagination<ScmOrderToReturnDto>>> GetScmOrderRequest([FromQuery] ProductSpecParams productSpecParams)
        {
            // Get AccEmployee
            var user = await _userManager.FindUserAsync(User);
            //GetAllOrder
            var inventoryOrders = await _accountingService.GetAllScmOrderRequstsAsync(productSpecParams, user.Id);
            //GetCount
            var Count = await _accountingService.GetCountOfScmOrderAsync(productSpecParams);

            var mappedInventoryOrders = _mapper.Map<IEnumerable<ScmOrder>, IEnumerable<ScmOrderToReturnDto>>(inventoryOrders);
            return Ok(new Pagination<ScmOrderToReturnDto>(productSpecParams.PageIndex, productSpecParams.PageSize, Count, mappedInventoryOrders));
        }


        #endregion

        #region Add Tax

        [HttpPost("AddTax")]
        public async Task<ActionResult<ApiResponseDto<TaxDto>>> CreateTax([FromBody] TaxDto tax)
        {
            var mappedTax = _mapper.Map<TaxDto, Tax>(tax);

            var result = await _accountingService.AddTaxAsync(mappedTax);

            if (result.Status == 200)
            {
                tax.Id = mappedTax.Id;
                return new ApiResponseDto<TaxDto>()
                {
                    Status = 200,
                    Message = "Created Successfully",
                    Data = tax
                };
            }
            else
            {
                return BadRequest(new { result?.Status, result?.Message });
            }
        }




        #endregion

        #region GET ALL Taxes
        [HttpGet("AllTaxes")]
        public async Task<IEnumerable<TaxDto>> GetTaxes()
        {
            var taxes = await _accountingService.GetAllTaxesAsync();
            var mappedTaxes = _mapper.Map<IEnumerable<Tax>, IEnumerable<TaxDto>>(taxes);
            return mappedTaxes;
        }

        #endregion
         

        #region Get All Invoices For Specific Supplier

        [HttpGet("AllInvoicesOfSupplier")]
        public async Task<ActionResult<IReadOnlyList<InvoiceDto>>> GetAllInvoicesForSpecificSupplier(int Id)
        {
            if (Id <= 0)
                return BadRequest(new { Status = 400, Message = "Invalid Supplier ID" });
            var Invoices = await _accountingService.GetAllInvoicesForSpecificSupplierAsync(Id);
            if (Invoices == null)
                return new List<InvoiceDto>();
            var mappedInvoices = _mapper.Map<IReadOnlyList<Invoice>, IReadOnlyList<InvoiceDto>>(Invoices);
            return Ok(mappedInvoices);
        }


        #endregion

        #region Get All Invoices related to vendors

        [HttpGet("AllInvoicesRelatedToVendors")]
        public async Task<ActionResult<IReadOnlyList<InvoiceDto>>> AllInvoicesRelatedToVendors()
        {
            // Get Login User
            var user = await _userManager.FindUserAsync(User);

            var Invoices = await _accountingService.GetAllInvoicesRelatedToVendors(user.Id);
            if (Invoices == null)
                return new List<InvoiceDto>();
            var mappedInvoices = _mapper.Map<IReadOnlyList<Invoice>, IReadOnlyList<InvoiceDto>>(Invoices);
            return Ok(mappedInvoices);
        }


        #endregion
        #region CreateInvoice

        [HttpPost("CreateInvoice")]
        public async Task<ActionResult<ApiResponseDto<CreateInvoiceToReturnDto>>> CreateInvoice([FromBody] CreateInvoiceDto invoiceDto)
        { 
            // Get Login User
            var user = await _userManager.FindUserAsync(User);
           
            var MappedInvoice = _mapper.Map<Invoice>(invoiceDto);
            var invoice =  await _accountingService.CreateInvoice(MappedInvoice,user.Id);
            if(invoice.Status!=200)
            {
                return BadRequest(new { invoice.Status, invoice.Message });
            }
            return Ok(
                new ApiResponseDto<CreateInvoiceToReturnDto>()
                {
                    Status = invoice.Status,
                    Message = invoice.Message,
                    Data = _mapper.Map< Invoice,CreateInvoiceToReturnDto>(invoice.Data)
                }


                ) ;
            

              

        }
        #endregion

        #region Get All Payments For Specific Supplier

        [HttpGet("AllPaymentsOfSupplier")]
        public async Task<ActionResult<IReadOnlyList<PaymentDetailsReturnDto>>> GetAllPaymentsForSpecificSupplier(int Id)
        {
            if (Id <= 0)
                return BadRequest(new { Status = 400, Message = "Invalid Supplier ID" });
            var Payments = await _accountingService.GetAllPaymentsForSpecificSupplierAsync(Id);
            if (Payments == null)
                return new List<PaymentDetailsReturnDto>();
            var mappedPayments = _mapper.Map<IReadOnlyList<Payment>, IReadOnlyList<PaymentDetailsReturnDto>>(Payments);
            return Ok(mappedPayments);
        }


        #endregion

        #region RegisterPayment
        [HttpPost("RegisterPayment")]
        public async Task<ActionResult<ApiResponseDto<PaymentDetailsReturnDto>>> RegisterPayment( PaymentDto payment)
        {
            if (payment.Amount <=0)
                return BadRequest(new { Status = 400, Message = "The amount must be greater than zero" }); 
            if (payment.InvoiceId <= 0)
                return BadRequest(new { Status = 400, Message = "The InvoiceId must be greater than zero" }); 
            if (payment.SupplierId <= 0)
                return BadRequest(new { Status = 400, Message = "The SupplierId must be greater than zero" });

            // Get Login User
            var user = await _userManager.FindUserAsync(User);

            var MappedPayment = _mapper.Map<Payment>(payment);
            MappedPayment.DoneBy = user.Id;

            var RegisterPayment = await _accountingService.RegisterPayment(MappedPayment);

            if (RegisterPayment.Status == 400)
                return BadRequest(new { RegisterPayment.Status, RegisterPayment.Message });
            if (RegisterPayment.Status == 500)
                return StatusCode(500, RegisterPayment.Message);

            return Ok(
                new ApiResponseDto<PaymentDetailsReturnDto>()
                {
                    Status = RegisterPayment.Status,
                    Message = RegisterPayment.Message,
                    Data = _mapper.Map<Payment, PaymentDetailsReturnDto>(MappedPayment)
                }
                );
        }
        #endregion

        #region payments related to vendors
        [HttpGet("PaymentsRelatedToVendors")]
        public async Task<ActionResult<IReadOnlyList<PaymentDetailsReturnDto>>> PaymentsRelatedToVendors([FromQuery] PaymentSpecParams paymentSpecParams)
        {
            // Get Login User
            var user = await _userManager.FindUserAsync(User);

            var Payments = await _accountingService.GetPaymentsRelatedToVendors(paymentSpecParams, user.Id);

            if (Payments is null)
                return new List<PaymentDetailsReturnDto>();
            var mappedPayments = _mapper.Map<IReadOnlyList<Payment>, IReadOnlyList<PaymentDetailsReturnDto>>(Payments);
            return Ok(mappedPayments);
        }
        #endregion
    }
}
