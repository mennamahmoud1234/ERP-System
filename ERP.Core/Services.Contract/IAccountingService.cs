using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Specifications.PayementSpec;
using ERP.Core.Specifications.Product_Spec;
using ERP.Core.Specifications.SupplierSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Services.Contract
{
     public  interface IAccountingService
    {
        Task<IEnumerable<InventoryOrder>> GetAllInventoryOrderRequstsAsync(ProductSpecParams productSpecParams, string Id);
        Task<IEnumerable<ScmOrder>> GetAllScmOrderRequstsAsync(ProductSpecParams productSpecParams, string Id);
        Task<int> GetCountOfScmOrderAsync(ProductSpecParams specParams);
        Task<ApiResponseDto<TaxDto>> AddTaxAsync(Tax tax);
        Task<IEnumerable<Tax>> GetAllTaxesAsync();
        Task<IReadOnlyList<Invoice>> GetAllInvoicesForSpecificSupplierAsync(int Id);
        Task<IReadOnlyList<Invoice>> GetAllInvoicesRelatedToVendors(string userId);
        Task<ApiResponseDto<Invoice>> CreateInvoice(Invoice invoice, string Id);
        
        #region Suppliers
        Task<IEnumerable<Supplier>> GetSuppliersAsync(SupplierSpecParams supplierSpecParams);
        Task<int> GetCountOfSuppliersAsync(SupplierSpecParams specParams);
        Task<Supplier?> GetSupplierAsync(int supplierId);
        #endregion
        Task<IReadOnlyList<Payment>> GetAllPaymentsForSpecificSupplierAsync(int id);
        Task<ApiResponseDto<PaymentDetailsReturnDto>> RegisterPayment(Payment payment);
        Task<IReadOnlyList<Payment>> GetPaymentsRelatedToVendors(PaymentSpecParams paymentSpecParams , string userId);
    }
}
