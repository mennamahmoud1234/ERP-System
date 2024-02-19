using ERP.Core;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Identity;
using ERP.Core.Services.Contract;
using ERP.Core.Specifications.InvoiceSpec;
using ERP.Core.Specifications.OrderSpec;
using ERP.Core.Specifications.PayementSpec;
using ERP.Core.Specifications.Product_Spec;
using ERP.Core.Specifications.ReplenishmentSpec;
using ERP.Core.Specifications.SCmOrderProduct;
using ERP.Core.Specifications.SupplierSpec;
using ERP.Core.Specifications.TaxSpec;
using ERP.Core.Specifications.UsersSpec;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Service
{
    public class AccountingService : IAccountingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInventoryService _inventoryService;
        private readonly ISCMService _sCMService;

        public AccountingService(IUnitOfWork unitOfWork ,IInventoryService inventoryService ,ISCMService sCMService) {
           _unitOfWork = unitOfWork;
           _inventoryService = inventoryService;
            _sCMService = sCMService;
            
        }

        public async  Task<IEnumerable<InventoryOrder>> GetAllInventoryOrderRequstsAsync(ProductSpecParams productSpecParams, string Id)
        {
            var spec = new InventoryOrderSpecification(productSpecParams,Id);
            var orders =  await _unitOfWork.Repositry<InventoryOrder>().GetAllWithSpecAsync(spec);
            return orders;
        }

        public  async Task<IEnumerable<ScmOrder>> GetAllScmOrderRequstsAsync(ProductSpecParams productSpecParams, string Id)
        {
            var spec= new ScmOrderSpec(productSpecParams,Id);
            var orders= await _unitOfWork.Repositry<ScmOrder>().GetAllWithSpecAsync(spec); 
            return orders;
            
        }

        public async Task<int> GetCountOfScmOrderAsync(ProductSpecParams specParams)
        {
            var Count = await _unitOfWork.Repositry<ScmOrder>().GetCountAsync();
            return Count;
        }

        public async Task<ApiResponseDto<TaxDto>> AddTaxAsync(Tax tax)
        {
            var TaxValidation = await TaxValidationAsync(tax.TaxName, tax.Id);

            //Check if Tax is not valid
            if (TaxValidation.Status != 200) return TaxValidation;

            //Add Tax
            await _unitOfWork.Repositry<Tax>().AddAsync(tax);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<TaxDto> { Status = 500, Message = "Failed To Add tax" };

            //if tax Created
            return new ApiResponseDto<TaxDto>
            {
                Status = 200,
                Message = "Added Successfully"
            };

        }

        public async Task<ApiResponseDto<TaxDto>> TaxValidationAsync(string taxName, int taxId)
        {
            //Get specific tax
            var TaxSpec = new OneTaxWithNameSpecifications(taxName);
            var SpecificTax = await _unitOfWork.Repositry<Tax>().GetWithSpecAsync(TaxSpec);
            // Check if there is product with same Name
            if (SpecificTax is not null && SpecificTax.Id != taxId)
                return new ApiResponseDto<TaxDto> { Status = 400, Message = "Invalid name , There is a tax with the same name" };


            return new ApiResponseDto<TaxDto> { Status = 200 };
        }

        public async Task<IEnumerable<Tax>> GetAllTaxesAsync()
        {
            //var spec = new TaxWithInvoicesUsedItSpecification();
            var taxes = await _unitOfWork.Repositry<Tax>().GetAllAsync();
            if (!taxes.Any()) return Enumerable.Empty<Tax>();
            else return taxes;
        }

        public Task<IReadOnlyList<Invoice>> GetAllInvoicesForSpecificSupplierAsync(int Id)
        {
            var spec = new InvoicesWithSpecificSupplier(Id);
            var Invoices = _unitOfWork.Repositry<Invoice>().GetAllWithSpecAsync(spec);
            return Invoices;
        }
        #region Suppliers
        public async Task<IEnumerable<Supplier>> GetSuppliersAsync(SupplierSpecParams supplierSpecParams)
        {
            var spec = new SupplierSpec(supplierSpecParams);
            var Supplier = await _unitOfWork.Repositry<Supplier>().GetAllWithSpecAsync(spec);
            return Supplier;
        }
        public async Task<Supplier?> GetSupplierAsync(int supplierId)
        {
            var spec = new SupplierSpec(supplierId);
            var Supplier = await _unitOfWork.Repositry<Supplier>().GetWithSpecAsync(spec);
            return Supplier;
        }
        public async Task<int> GetCountOfSuppliersAsync(SupplierSpecParams specParams)
        {
            var countSpec = new SupplierForCountSpecifications(specParams);
            var Count = await _unitOfWork.Repositry<Supplier>().GetCountAsync(countSpec);
            return Count;
        }


        #endregion

        
        public async Task<ApiResponseDto<Invoice>> CreateInvoice(Invoice invoice ,string Id)
        {
            // Check Supplier
            var supplier =   await GetSupplier(invoice.SupplierId);
            if (supplier == null) return new ApiResponseDto<Invoice>() { Status = 400, Message = "This Supplier Not Exist" };
            //Check Employee 
            var emp =  await CheckUser(Id);
            if (emp == null) return new ApiResponseDto<Invoice>() { Status = 400, Message = "This Employee Not Exist" };
            invoice.OrderBy = Id;
            // Check TaxId Exist
            var tax = await GetTax(invoice.TaxID);
            if ( tax== null) return new ApiResponseDto<Invoice>() { Status = 400, Message = "This Tax Not Exist" };
            invoice.TaxValue = tax.TaxValue;
            //Check Inventory && ScmId not equal null
            if (invoice.ScmOrderId == null && invoice.InventoryOrderId == null) return new ApiResponseDto<Invoice>() { Status = 400, Message = "You Should Enter inventoryOrderId Or ScmOrderId" };
            if (invoice.ScmOrderId != null && invoice.InventoryOrderId != null) return new ApiResponseDto<Invoice>() { Status = 400, Message = "You Should Enter  only inventoryOrderId Or ScmOrderId" };

            #region InventoryOrderInvoice
            // check inventoryOrderDetails
            if (invoice.InventoryOrderId != null)
            {
                // CheckInventoryOrderId  exist in inventoryOrder table
                var InventoryOrder = await _unitOfWork.Repositry<InventoryOrder>().GetAsync(invoice.InventoryOrderId.Value);
                if (InventoryOrder == null) return new ApiResponseDto<Invoice>() { Status = 400, Message = " This Inventory Order Not Exist " };
                // CheckInventoryOrderId exist in Invoice table
                var InventoryOrderInvoice = await _unitOfWork.Repositry<Invoice>().GetWithSpecAsync(new InvoiceInventorySpec(invoice.InventoryOrderId.Value));
                if (InventoryOrderInvoice != null) return new ApiResponseDto<Invoice>() { Status = 400, Message = "There Is Invoice For This Inventory Order" };
             
                // Get Specific Order
                var inventoryOrder = await _inventoryService.GetInventoryOrderAsync(invoice.InventoryOrderId.GetValueOrDefault());
                // Get Price of product && Quantity
                var productPrice = inventoryOrder?.Data?.Product?.ProductCostPrice;
                var ProductQuantity = inventoryOrder?.Data.Quantity;
                // Calc Total Price = productPrice+TaxValue
                invoice.Total = (productPrice.Value * ProductQuantity.Value) + invoice.TaxValue;
                // Calc Payed ??


            }
            #endregion

            #region ScmOrderInvoice
            // check inventoryOrderDetails
            if (invoice.ScmOrderId != null)
            {
                // CheckScmOrderId  exist in ScmOrder table
                var scmOrder = await _unitOfWork.Repositry<ScmOrder>().GetAsync(invoice.ScmOrderId.Value);
                if (scmOrder == null) return new ApiResponseDto<Invoice>() { Status = 400, Message = " This ScmOrder Not Exist " };
                // CheckScmOrderId exist in Invoice table
                var ScmOrderInvoice = await _unitOfWork.Repositry<Invoice>().GetWithSpecAsync(new InvoiceScmOrderSpec(invoice.ScmOrderId.Value));
                if (ScmOrderInvoice != null) return new ApiResponseDto<Invoice>() { Status = 400, Message = "There Is Invoice For This ScmOrder" };
                // Get Specific ScmOrder
                var order = await _unitOfWork.Repositry<ScmOrder>().GetWithSpecAsync(new ScmOrderSpec(invoice.ScmOrderId.Value));
                //Get All Product In This Order
                if (order != null)
                {
                    foreach (var item in order.ScmOrderProducts)
                    {
                        // Get Price of product && Quantity
                        var productPrice = item.Product.ProductCostPrice;
                        var ProductQuantity = item.Quantity;
                        // Calc Total Price = productPrice+TaxValue
                        invoice.Total += (productPrice * ProductQuantity) + invoice.TaxValue;
                        
                    }
                }
                



            }
            #endregion


            await _unitOfWork.Repositry<Invoice>().AddAsync(invoice);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<Invoice> { Status = 500, Message = "Failed To Create Invoice" };

            //if tax Created
            return new ApiResponseDto<Invoice>
            {
                Status = 200,
                Message = " CreatedSuccessfully",
                Data= invoice
            };
        }


        // Get Supllier
        public async Task<Supplier?> GetSupplier(int Id)
        {
            var supplier = await _unitOfWork.Repositry<Supplier>().GetAsync(Id);
            return supplier;
        }

        public async Task<Employee?> CheckUser(string userId)
        {
            //Get specific User
            var UseSpec = new UserSpec(userId);
            var SpecificUser = await _unitOfWork.Repositry<Employee>().GetWithSpecAsync(UseSpec);
            return SpecificUser;
        }

        public async Task<Tax?> GetTax(int Id)
        {
            var tax = await _unitOfWork.Repositry<Tax>().GetAsync(Id);
            return tax;
        }
        public async Task<Invoice?> GetInvoice(int Id)
        {
            var tax = await _unitOfWork.Repositry<Invoice>().GetAsync(Id);
            return tax;
        }

        public async Task<IReadOnlyList<Payment>> GetAllPaymentsForSpecificSupplierAsync(int id)
        {
            var spec = new PaymentWithSpecificSupplier(id);
            var Payments =await _unitOfWork.Repositry<Payment>().GetAllWithSpecAsync(spec);
            return Payments;
        }
        public async Task<IReadOnlyList<Invoice>> GetAllInvoicesRelatedToVendors(string userId)
        {
            var spec = new InvoiceInventorySpec(userId);
            var Invoice = await _unitOfWork.Repositry<Invoice>().GetAllWithSpecAsync(spec);
            return Invoice;
        }
        #region Register Payment
        public async Task<ApiResponseDto<PaymentDetailsReturnDto>> RegisterPayment(Payment payment)
        {
            // Check Supplier
            var supplier = await GetSupplier(payment.SupplierId);
            if (supplier == null) return new ApiResponseDto<PaymentDetailsReturnDto>() { Status = 400, Message = "This Supplier Not Exist" };

            // Check InvoiceId Exist
            var invoice = await GetInvoice(payment.InvoiceId);
            if (invoice == null) return new ApiResponseDto<PaymentDetailsReturnDto>() { Status = 400, Message = "This Invoice Not Exist" };
            //update invoice Paid
            invoice.Paid = invoice.Paid + payment.Amount;
            invoice.ToPay = invoice.Total - invoice.Paid;
            await _unitOfWork.Repositry<Payment>().AddAsync(payment);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<PaymentDetailsReturnDto> { Status = 500, Message = "Failed To Register Payment" };

            //if tax Created
            return new ApiResponseDto<PaymentDetailsReturnDto>
            {
                Status = 200,
                Message = " CreatedSuccessfully"
            };
        }

        #endregion

        #region Get payments related to vendors:
        public async Task<IReadOnlyList<Payment>> GetPaymentsRelatedToVendors(PaymentSpecParams paymentSpecParams , string userId)
        {
            //Get Payments
            var PaymentsSpec = new PayementSpec(paymentSpecParams , userId);
            var Payments = await _unitOfWork.Repositry<Payment>().GetAllWithSpecAsync(PaymentsSpec);
            return Payments;

        }

        #endregion
    }
}

